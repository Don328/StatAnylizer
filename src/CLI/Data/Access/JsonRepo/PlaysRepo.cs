using Newtonsoft.Json;
using StatAnylizer.Core.Data.Access.Interfaces;
using StatAnylizer.Core.Data.Records;
using StatAnylizer.Core.Entities;
using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.JsonRepo;

public class PlaysRepo : IAccessPlays
{
    private const string path =
        "./Data/Store/plays.json";

    // Update existing play record. Save if new
    public int Save(Play play)
    {
        

        var records = GetRecords().ToList();
        var record =
            (from r in records
             where r.Id == play.Id
             select r).FirstOrDefault();

        if (play.Id < 1)
            play.Id = IdTableRepo
                .GetNextPlayId();

        if (record != null)
            records.Remove(record);

        records.Add(play.ToRecord());

        var json = JsonConvert.SerializeObject(records);
        using var writer = new StreamWriter(path);
        writer.Write(json);

        return play.Id;
    }

    public IEnumerable<Play> GetByGame(int gameId)
    {
        var records =
            (from r in GetRecords()
             where r.GameId == gameId
             select r).ToList();

        var plays = ConvertRecords(records);
        return plays;
    }

    public IEnumerable<Play> GetByDrive(int driveId)
    {
        var records =
            (from r in GetRecords()
             where r.DriveId == driveId
             select r).ToList();

        var plays = ConvertRecords(records);
        return plays;
    }

    public IEnumerable<Play> GetByTeam(int teamId)
    {
        var records =
            (from r in GetRecords()
             where r.OffenseId == teamId
             select r).ToList();
        var plays = ConvertRecords(records);
        return plays;
    }

    private static IEnumerable<PlayRecord> GetRecords()
    {
        try
        {
            using StreamReader reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject <List<PlayRecord>>(json)?? new();
        }
        catch(Exception e)
        {
            Console.WriteLine();
            Console.WriteLine("There was a problem fetching the play records from the file.");
            Console.WriteLine(e.Message);
            Console.WriteLine();
            return new List<PlayRecord>();
        }
    }

    private static IEnumerable<Play> ConvertRecords(List<PlayRecord> records)
    {
        var plays = new List<Play>();
        foreach (var record in records)
        {
            var play = new Play(record);
            plays.Add(play);
        }

        return plays;
    }

    public void Dispose(){ }
}
