using StatAnylizer.Core.Data.Access.JsonRepo;
using StatAnylizer.Core.Entities;
using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Interface.Prompt;

internal static class AddPlayers
{
    internal static void Prompt(Play play)
    {
        switch (play.PlayType)
        {
            case PlayType.Kickoff:
                PromptForKicker(play, play.DefenseId);
                PromptForReturner(play, play.OffenseId);
                break;
            case PlayType.Pass:
                PromptForPasser(play);
                PromptForReciever(play);
                break;
            case PlayType.Incomplete:
                PromptForPasser(play);
                PromptForReciever(play);
                break;
            case PlayType.Sack:
                PromptForPasser(play);
                break;
            case PlayType.Interception:
                PromptForPasser(play);
                PromptForInterceptingPlayer(play);
                break;
            case PlayType.Run:
                PromptForRusher(play);
                break;
            case PlayType.Fumble:
                PromptForFumblingPlayer(play);
                PrompForRecoveringPlayer(play);
                break;
            case PlayType.Penalty:
                /* Penalty player data is handled by
                 * PromptForPenalty() called from the 
                 * NewPenalty prompt */
                break;
            case PlayType.FieldGoal:
                PromptForKicker(play, play.OffenseId);
                break;
            case PlayType.Punt:
                PromptForPunter(play);
                break;
            case PlayType.XPK:
                PromptForKicker(play, play.OffenseId);
                break;
            case PlayType.XPR:
                PromptForRusher(play);
                break;
            case PlayType.XPP:
                PromptForPasser(play);
                PromptForReciever(play);
                break;
            case PlayType.Kneel: break;
            default: break;
        }

        PromptForTacklers(play);
    }

    public static void PromptForPenalty(Penalty penalty, int teamId)
    {
        penalty.PlayerId = GetPlayerId("penalized player", teamId);
    }

    private static int? GetPlayerId(string target, int teamId)
    {
        DisplayPlayers(teamId);
        Console.WriteLine("0 --> [New Player]");
        Console.WriteLine("[LEAVE BLANK] --> No Player");
        Console.WriteLine();
        Console.WriteLine($"Select the {target}");
        Console.WriteLine("Enter the Id to select a player from the list.");
        var response = Console.ReadLine() ?? "-1";
        if (response == "") return null;
        var success = Int32.TryParse(response, out int playerId);
        if (success)
        {
            if (playerId == -1) return null;
            if (playerId == 0)
            {
                var newPlayer = new Player(teamId);
                NewPlayer.Prompt(newPlayer);
            }
            return playerId;
        }

        return GetPlayerId(target, teamId);
    }

    private static void DisplayPlayers(int teamId)
    {
        Console.WriteLine();

        var playersRepo = new PlayersRepo();
        var players = playersRepo.GetByTeam(teamId).ToList();
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Id} {player.FirstName} {player.LastName}");
        }
    }

    private static void PromptForKicker(Play play, int teamId)
    {
        var id = GetPlayerId("kicker", teamId);
        play.KickerId = id;
    }

    private static void PromptForReturner(Play play, int teamId)
    {
        var id = GetPlayerId("returner", teamId);
        play.ReturnerId = id;
    }

    private static void PromptForPasser(Play play)
    {
        var id = GetPlayerId("passer", play.OffenseId);
        play.PasserId = id;
    }

    private static void PromptForReciever(Play play)
    {
        var id = GetPlayerId("reciver", play.OffenseId);
        play.RecieverId = id;
    }

    private static void PromptForInterceptingPlayer(Play play)
    {
        var id = GetPlayerId("intercepting player", play.DefenseId);
        play.InterceptingPlayerId = id;
    }

    private static void PromptForRusher(Play play)
    {
        var id = GetPlayerId("rusher", play.OffenseId);
        play.RunnerId = id;
    }

    private static void PromptForFumblingPlayer(Play play)
    {
        var id = GetPlayerId("fumbling player", play.OffenseId);
        play.FumbleForcePlayerId = id;
    }

    private static void PrompForRecoveringPlayer(Play play)
    {
        var id = GetPlayerId("recovering player", play.DefenseId);
        play.RecoveringPlayerId = id;
    }

    private static void PromptForPunter(Play play)
    {
        var id = GetPlayerId("punter", play.OffenseId);
        play.PunterId = id;
    }

    private static void PromptForTacklers(Play play)
    {
        var tacklerId = GetPlayerId("tackler (blank to exit)", play.DefenseId) ?? -1;
        if (tacklerId == -1) return;
        play.TacklingPlayersId =
            play.TacklingPlayersId
            .Append(tacklerId);
        PromptForTacklers(play);
    }
}
