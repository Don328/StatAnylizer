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

public class PlayersRepo : IAccessPlayers
{
    private const string path =
    "./Data/Store/players.json";

    // Update existing player record. Save if new
    public int Save(Player player)
    {
        var records = GetPlayerRecords().ToList(); ;
        var record =
            (from r in records
             where r.Id == player.Id
             select r).FirstOrDefault();

        if (player.Id < 1)
        {
            player.Id = IdTableRepo.GetNextPlayerId();
        }

        if (record != null)
        {
            records.Remove(record);
        }

        records.Add(player.ToRecord());

        var json = JsonConvert.SerializeObject(records);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);

        return player.Id;
    }

    public IEnumerable<Player> GetAll()
    {
        var records = GetPlayerRecords();
        var players = ConvertRecords(records);
        return players;
    }

    public IEnumerable<Player> GetByTeam(int teamId)
    {
        var playerRecords = GetPlayerRecords().ToList();
        var teamRecords =
            (from p in playerRecords
             where p.TeamId == teamId
             select p).ToList();

        var teamPlayers = new List<Player>();
        foreach (var record in teamRecords)
        {
            var player = new Player(record);
            teamPlayers.Add(player);
        }

        return teamPlayers;
    }

    private static IEnumerable<PlayerRecord> GetPlayerRecords()
    {
        try
        {
            using StreamReader reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            var records = JsonConvert.DeserializeObject<List<PlayerRecord>>(json) ?? new();

            return records;
        }
        catch
        {
            Console.WriteLine();
            Console.WriteLine("Error fetching player records from file.");
            Console.WriteLine();

            return new List<PlayerRecord>();
        }
    }

    private static IEnumerable<Player> ConvertRecords(IEnumerable<PlayerRecord> records)
    {
        var players = new List<Player>();
        foreach (var record in records)
        {
            var player = new Player(record);
            players.Add(player);
        }

        return players;
    }
}
