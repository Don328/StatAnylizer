using StatAnylizer.Core.Data.Access.JsonRepo;
using StatAnylizer.Core.Entities;
using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Interface.Prompt;

/// <summary>
/// TODO: Add exception handling to promts when parsing responses
/// </summary>

internal static class NewDrive
{
    internal static bool Prompt(Drive drive, int quarter, out int newQuarter)
    {
        PromptForDriveData(drive);
        drive.Id = SaveDrive(drive);
        newQuarter = GetPlays(drive, quarter);

        if (drive.LastDriveOfGame)
        {
            return true;
        }

        return false;
    }

    private static int GetPlays(Drive drive, int quarter)
    {
        bool driveComplete = false;
        int? previousPlayId = null;

        while (!driveComplete)
        {
            var play = new Play(drive);
            play.Quarter = quarter;
            
            if (previousPlayId is not null)
            {
                play.PreviousPlayId = previousPlayId;
            }

            bool newQuarter = NewPlay.Prompt(play, out previousPlayId);
            if (newQuarter) quarter++;
            driveComplete = play.EndOfDrive;                
        }

        return quarter;
    }

    private static int SaveDrive(Drive drive)
    {
        var repo = new DrivesRepo();
        return repo.Save(drive);

    }

    private static void PromptForDriveData(Drive drive)
    {
        PromptForNumberOfPlays(drive);
        PromptForStartingLOS(drive);
        PromptForYardsGained(drive);
        PromptForTimeOfPossession(drive);
        PromptForDriveResult(drive);
        if (drive.Result == DriveResult.EndOfGame) drive.LastDriveOfGame = true;
        else PromptForGameOver(drive);
    }

    private static void PromptForNumberOfPlays(Drive drive)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the number of plays in the drive:");
        var response = Console.ReadLine();

        // Add exception handling
        var success = Int32.TryParse(response, out int playCount);
        if (success) drive.NumberOfPlays = playCount;
    }

    private static void PromptForStartingLOS(Drive drive)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the starting line of scrimmage for the drive (after kickoff).");
        Console.WriteLine("Use negative numbers for yard lines on the offense defending side.");
        Console.WriteLine("For instance, a touchback will typically result in a line of scrimmage");
        Console.WriteLine("value of -25");
        var response = Console.ReadLine();

        // Add exception handling
        var succeess = Int32.TryParse(response, out int lineOfScrimmage);
        if (succeess) drive.StartingLOS = lineOfScrimmage;
    }

    private static void PromptForYardsGained(Drive drive)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the total yards gained for the drive:");
        var response = Console.ReadLine();

        // Add exception handling
        var success = Int32.TryParse(response, out int yardsGained);
        if (success) drive.TotalYards = yardsGained;

    }

    private static void PromptForTimeOfPossession(Drive drive)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the time of possession for the drive:");
        Console.WriteLine("[Format: m:ss]");
        var response = Console.ReadLine();
        var zeroHour = new StringBuilder("0:");
        var timeSpanString = zeroHour
            .Append(response).ToString();
        
        // Add exception handling
        var success = TimeSpan
            .TryParse(
                timeSpanString,
                out TimeSpan timeOfPossession);
        if (success) drive.TimeOfPossession = timeOfPossession;
    }

    private static void PromptForDriveResult(Drive drive)
    {
        Console.WriteLine();
        var lastValue = (int)DriveResult.EndOfGame;
        int i = 0;
        while (i <= lastValue)
        { 
            Console.WriteLine($"{i} - {(DriveResult)i}");
            i++;
        }
        Console.WriteLine();
        Console.WriteLine("Enter the value for the result of the drive:");
        var response = Console.ReadLine();

        // Add Exception handling
        var success = Int32.TryParse(response, out int result);
        if (success) drive.Result = (DriveResult)result;
    }

    private static void PromptForGameOver(Drive drive)
    {
        Console.WriteLine();
        Console.WriteLine("Is this the last drive of the game? y/n:");
        var response = Console.ReadLine()?? "";

        if (response.ToLower() == "y")
            drive.LastDriveOfGame = true;
        if (response.ToLower() == "n")
            drive.LastDriveOfGame = false;
        else PromptForGameOver(drive);
    }
}
