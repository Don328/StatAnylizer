using StatAnylizer.Core.Data.Access.JsonRepo;
using StatAnylizer.Core.Entities;
using StatAnylizer.Core.Interface.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Interface.Prompt;

internal static class NewGame
{
    public static void Prompt()
    {
        PromptForSeason();
        var week = PromptForWeek();
        var homeId = PromptForHomeTeam();
        var awayId = PromptForAwayTeam();
        var game = new Game(State.GetSeason(), week, homeId, awayId);
        game.Id = SaveGame(game);


        game.Drives = GetDrives(game);
    }

    internal static void PromptForSeason()
    {
        Console.WriteLine();
        Console.WriteLine("For what season? (enter 4 digit year)");
        var response = Console.ReadLine();
        try
        {
            int season;
            var success = Int32.TryParse(response, out season);
            if (season < 2022 || season > DateTime.Now.Year) success = false;
            if (success)
            {
                State.SetSeason(season);
                return;
            }

            throw new Exception(message: "Season must be no greater than the current year and no less than '2022'.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
            PromptForSeason();
        }
    }

    private static int PromptForWeek()
    {
        Console.WriteLine();
        Console.WriteLine("Enter the week of the NFL season (1 thru 18)");
        var response = Console.ReadLine();
        try
        {
            int week;
            var success = Int32.TryParse(response, out week);
            if (week < 0 || week > 18) success = false;
            if (success) return week;

            throw new Exception(message: "Week value must be a number from 1 to 18.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return PromptForWeek();
        }
    }

    private static int PromptForHomeTeam()
    {
        Console.WriteLine();
        Console.WriteLine("Enter the Id, Name, City, or Abbreviation for the home team.");
        Console.WriteLine("For the list of available teams, leave blank");
        var response = Console.ReadLine()?? "";
        if (string.IsNullOrEmpty(response))
        {
            Teams.Show(State.Teams);
            PromptForHomeTeam();
        }

        var id = ParseTeamId(response);
        if (id == null) return PromptForHomeTeam();
        else return (int)id;
       

    }

    private static int PromptForAwayTeam()
    {
        Console.WriteLine();
        Console.WriteLine("Enter the Id, Name, City, or Abbreviation for the away team.");
        Console.WriteLine("For the list of available teams, leave blank");
        var response = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(response))
        {
            Teams.Show(State.Teams);
            PromptForAwayTeam();
        }

        var id = ParseTeamId(response);
        if (id == null) return PromptForAwayTeam();
        else return (int)id;
    }

    private static int? ParseTeamId(string input)
    {
        try
        {
            int id;
            var success = Int32.TryParse(input, out id);
            if (id < 0 || id > 31) success = false;
            if (success) return id;

            throw new Exception(message: "Team Id must be a vailid team Id number (0 - 31)");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    private static int SaveGame(Game game)
    {
        var repo = new GamesRepo();
        return repo.Save(game);
    }

    private static IEnumerable<Drive> GetDrives(Game game)
    {
        var drives = new List<Drive>();
        int quarter = 1;
        while (!game.IsFinal)
        {
            var idArray = PromptForTeamInPossession(game);
            var drive = new Drive(game.Id, idArray);
            game.IsFinal = NewDrive.Prompt(drive, quarter, out quarter);
        }

        return drives;
    }

    private static int[] PromptForTeamInPossession(Game game)
    {
        var teamsRepo = new TeamsRepo();
        var away = teamsRepo.GetById(game.AwayId);
        var home = teamsRepo.GetById(game.HomeId);
        Console.WriteLine();
        Console.WriteLine($"0 - {away.City} {away.Name}");
        Console.WriteLine($"1 - {home.City} {home.Name}");
        Console.WriteLine("Enter '0' or '1' for the team in possesion for this drive:");
        var result = Console.ReadLine();

        // Add exception handling
        var success = Int32.TryParse(result, out int offenseSelector);
        
        if (success)
        {
            switch(offenseSelector)
            {
                case 0:
                    return new int[] { away.Id, home.Id };
                case 1:
                    return new int[] { home.Id, away.Id };
            }
        }

        return new int[] { -1, -1};
    }

}
