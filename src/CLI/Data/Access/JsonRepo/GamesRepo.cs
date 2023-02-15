using Newtonsoft.Json;
using StatAnylizer.Core.Data.Access.Interfaces;
using StatAnylizer.Core.Data.Records;
using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.JsonRepo;

public class GamesRepo : IAccessGames
{
    private const string path =
        "./Data/Store/games.json";

    // Update existing game record. Save if new
    public int Save(Game game)
    {
        var records = GetRecords().ToList();

        if (game.Id < 1)
            game.Id = IdTableRepo
                .GetNextGameId();

        var record =
            (from r in records
             where r.Id == game.Id
             select r).FirstOrDefault();

        if (record != null)
            records.Remove(record);

        records.Add(game.ToRecord());

        var json = JsonConvert.SerializeObject(records);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);

        return game.Id;
    }


    public IEnumerable<Game> GetSeason()
    {
        // Move this try/catch block into 'GetRecords()'

        try
        {
            List<GameRecord> records = GetRecords().ToList();
            var seasonRecord =
                (from r in records
                 where r.Season == State.GetSeason()
                 select r).ToList();

            var games = new List<Game>();
            foreach (var record in seasonRecord)
            {
                var game = new Game(record);
                games.Add(game);
            }

            return games;
        }
        catch (Exception e)
        {
            ThrowException(e);
            return new List<Game>();
        }
    }

    public IEnumerable<Game> GetWeek(int week)
    {
        try
        {
            var seasonGames = GetSeason();
            var weekGames = (from g in seasonGames
                             where g.Week == week
                             select g).ToList();

            if (weekGames != null)
            {
                return weekGames;
            }

            throw new Exception(message: $"Games for season: {State.GetSeason()}, week: {week} not found.");
        }
        catch (Exception e)
        {
            ThrowException(e);
            return new List<Game>();
        }
    }

    public IEnumerable<Game> GetTeamSeason(int teamId)
    {
        try
        {
            var seasonGames = GetSeason();
            var teamSeason = (from g in seasonGames
                              where g.HomeId == teamId
                              || g.AwayId == teamId
                              select g).ToList();

            if (teamSeason != null)
            {
                return teamSeason;
            }

            throw new Exception(message: $"List of games for teamId: {teamId}, season: {State.GetSeason()} not found.");
        }
        catch (Exception e)
        {
            ThrowException(e);
            return new List<Game>();
        }
    }

    public Game? GetTeamWeek(int week, int teamId)
    {
        try
        {
            var seasonGames = GetWeek(week);
            var teamSeason = (from g in seasonGames
                              where g.HomeId == teamId
                              || g.AwayId == teamId
                              select g).First();

            if (teamSeason != null)
            {
                return teamSeason;
            }

            throw new Exception(message: $"game for teamId: {teamId}, week: {week} not found.");
        }
        catch (Exception e)
        {
            ThrowException(e);
            return null;
        }
    }

    public Game? GetById(int id)
    {
        try
        {
            var seasonGames = GetSeason();
            var game = (from g in seasonGames
                        where g.Id == id
                        select g).FirstOrDefault();
            if (game != null)
            {
                return game;
            }

            throw new Exception(message: $"Game with Id: {id} not found.");
        }
        catch (Exception e)
        {
            ThrowException(e);
            return null;
        }
    }
    private static IEnumerable<GameRecord> GetRecords()
    {
        using StreamReader reader = File.OpenText(path);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<GameRecord>>(json) ?? new();
    }

    private static void ThrowException(Exception e)
    {
        Console.WriteLine();
        Console.WriteLine("Error reading games list from file.");
        Console.WriteLine(e.Message);
        Console.WriteLine();
    }
}
