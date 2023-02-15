using StatAnylizer.Core.Data.Access.Interfaces;
using StatAnylizer.Core.Data.Access.JsonRepo;
using StatAnylizer.Core.Entities;
using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Interface.Prompt;

internal static class NewPlay
{
    internal static bool Prompt(Play play, out int? playId)
    {
        PromptForPlayData(play);
        AddPlayers.Prompt(play);
        play.Id = SavePlay(play);
        playId = play.Id;

        return play.EndOfDrive;
    }

    private static int SavePlay(Play play)
    {
        using IAccessPlays repo = new PlaysRepo();
        return repo.Save(play);
    }

    private static void PromptForPlayData(Play play)
    {
        PromptForDown(play);
        PromptForDistanceToGo(play);
        PromptForLOS(play);
        PromptForGameClock(play);
        PromptForPlayType(play);
        if (play.PlayType == PlayType.Penalty)
            PromptForPenaltyData(play);
        PromptForYardsGained(play);
        PromptForTimeElapsed(play);
        CheckPlayResult(play);
    }

    private static void PromptForPenaltyData(Play play)
    {
        var penalty = new Penalty(
            play.GameId,
            play.DriveId,
            play.Id);

        penalty.AgainstOffense = NewPenalty.IsOnOffense();
        if (penalty.AgainstOffense) penalty.TeamId = play.OffenseId;
        else penalty.TeamId = play.DefenseId;

        play.PenaltyId = NewPenalty.Prompt(penalty);
    }

    private static void PromptForPlayType(Play play)
    {
        Console.WriteLine();
        Console.WriteLine("Play Types:");

        for (int i = 0; i <= (int)PlayType.Kneel; i++)
        {
            Console.WriteLine($"{i} - {(PlayType)i}");
        }

        Console.WriteLine();
        Console.WriteLine("Enter the value for the type of play to be entered (0 - 13):");
        var response = Console.ReadLine();

        var success = Int32.TryParse(response, out int playTypeValue);
        if (playTypeValue < 0) success = false;
        if (playTypeValue > (int)PlayType.Kneel) success = false;
        if (success)
        {
            play.PlayType = (PlayType)playTypeValue;
        }
    }

    private static void PromptForDown(Play play)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the down (1-4):");
        var response = Console.ReadLine();

        // Add exception handling
        var success = Int32.TryParse(response, out int down);
        if (success) play.Down = down;
    }

    private static void PromptForDistanceToGo(Play play)
    {
        Console.WriteLine();
        Console.WriteLine("Enter Distance for a first down or touchdown:");
        var response = Console.ReadLine();

        // Add exception handling
        var success = Int32.TryParse(response, out int distance);
        if (success) play.Distance = distance;
    }
    
    private static void PromptForLOS(Play play)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the line of scrimmage for the play.");
        Console.WriteLine("For yardlines on the offenses definding side, use negative numbers.");
        var response = Console.ReadLine();

        // Add exception handling
        var success = Int32.TryParse(response, out int yardline);
        if (success) play.LineOfScrimmage = yardline;
    }
    
    private static void PromptForYardsGained(Play play)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the yards gained on the play.");
        Console.WriteLine("For plays resulting in a loss, use negative numbers.");
        var response = Console.ReadLine();

        // Add exception handling
        var success = Int32.TryParse(response, out int yardsGained);
        if (success) play.NetYards = yardsGained;

    }

    private static void CheckPlayResult(Play play)
    {
        if (play.NetYards == play.Distance)
        {
            if (play.NetYards == play.LineOfScrimmage)
            {
                if (play.LineOfScrimmage > 0)
                {
                    play.IsTouchdown = true;
                    return;
                }
            }

            play.FirstDownGained = true;
        }
    }

    private static bool PromptForTimeElapsed(Play play)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the game clock for the following play");
        Console.WriteLine("Enter '0:0' for end of half, end of 4th or end of game");
        Console.WriteLine("[Format: m:ss]");
        var response = Console.ReadLine();
        var zeroHour = new StringBuilder("0:");
        var timeSpanString = zeroHour
            .Append(response).ToString();

        // Add exception handling
        var success = TimeSpan
            .TryParse(
                timeSpanString,
                out TimeSpan nextPlayClock);

        if (success)
        {
            if (nextPlayClock > play.GameClock)
            {
                play.EndOfQuarter = true;
                var quarterOffset = new TimeSpan(0, 15, 0) - nextPlayClock;
                play.TimeElapsed = play.GameClock + quarterOffset;
                return play.EndOfQuarter;
            }
            
            var zero = new TimeSpan(0, 0, 0);
            if (nextPlayClock.Equals(zero))
            {
                play.TimeElapsed = play.GameClock;
                play.EndOfQuarter = true;
                switch(play.Quarter)
                {
                    case 2:
                        play.EndOfHalf = true;
                        play.EndOfDrive = true;
                        break;
                    case 4:
                        play.EndOfGame = PromptForEndOfGame();
                        play.EndOf4th = true;
                        play.EndOfDrive = true;
                        break;
                    case 5:
                        play.EndOfGame = PromptForEndOfGame();
                        break;
                    default:
                        break;
                }
                return play.EndOfQuarter;
            }

            play.TimeElapsed = play.GameClock - nextPlayClock;
        }
            
        return play.EndOfQuarter;
    }

    private static bool PromptForEndOfGame()
    {
        Console.WriteLine();
        Console.WriteLine("Last play of game? (y/n):");
        var response = Console.ReadLine()?? "";

        switch(response.ToLower())
        {
            case "y":
                return true;
            case "n":
                return false;
            default:
                Console.WriteLine();
                Console.WriteLine("****************");
                Console.WriteLine("Enter 'y' or 'n'");
                Console.WriteLine("****************");
                Console.WriteLine();
                return PromptForEndOfGame();
        }
    }

    private static void PromptForGameClock(Play play)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the play game clock");
        Console.WriteLine("[Format: m:ss]");
        var response = Console.ReadLine();
        var zeroHour = new StringBuilder("0:");
        var timeSpanString = zeroHour
            .Append(response).ToString();

        // Add exception handling
        var success = TimeSpan
            .TryParse(
                timeSpanString,
                out TimeSpan gameClock);
        if (success) play.GameClock = gameClock;
    }
}

