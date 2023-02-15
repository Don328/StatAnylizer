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

public class DrivesRepo : IAccessDrives
{
    private const string path =
        "./Data/Store/drives.json";

    // Update existing drive record. Save if new
    public int Save(Drive drive)
    {
        var records = GetRecords().ToList();
        var record =
            (from r in records
             where r.Id == drive.Id
             select r).FirstOrDefault();

        if (drive.Id < 1)
            drive.Id = IdTableRepo
                .GetNextDriveId();

        if (record != null)
            records.Remove(record);

        records.Add(drive.ToRecord());
        
        var json = JsonConvert.SerializeObject(records);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);

        return drive.Id;
    }

    public IEnumerable<Drive> GetGame(int gameId)
    {
        try
        {
            using StreamReader reader = File.OpenText(path);
            var json = reader.ReadToEnd();
            List<DriveRecord> records = JsonConvert.DeserializeObject<List<DriveRecord>>(json) ?? new();

            var gameDriveRecords =
                (from r in records
                 where r.GameId == gameId
                 select r).ToList();

            List<Drive> drives = new();
            foreach (DriveRecord record 
                in gameDriveRecords)
            {
                var drive = new Drive(record);
                drives.Add(drive);
            }

            return drives;

        }
        catch (Exception e)
        {
            Console.WriteLine();
            Console.WriteLine(e.Message);
            Console.WriteLine("Error reading teams list from file.");
            Console.WriteLine();
            return new List<Drive>();
        }
    }

    private static IEnumerable<DriveRecord> GetRecords()
    {
        using var reader = new StreamReader(path);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<DriveRecord>>(json)?? new();
    }
}
