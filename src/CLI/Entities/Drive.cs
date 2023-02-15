using StatAnylizer.Core.Data.Records;
using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Entities;

public class Drive : IEntity<DriveRecord>
{
    public Drive(DriveRecord record)
    {
        Id = record.Id;
        GameId = record.GameId;
        OffenseId = record.OffenseId;
        DefenseId = record.DefenseId;
        TimeOfPossession = record.TimeOfPossession;
        Result = (DriveResult)record.ResultIndex;
        StartingLOS = record.StartingLOS;
        TotalYards = record.TotalYards;
        NumberOfPlays = record.NumberOfPlays;
        LastDriveOfGame = record.LastDriveOfGame;
    }

    public Drive(
        int gameId,
        int[] teamIds)
    {
        GameId = gameId;
        OffenseId = teamIds[0];
        DefenseId = teamIds[1];
    }

    public int Id { get; set; }
    public int GameId { get; set; }
    public int OffenseId { get; set; }
    public int DefenseId { get; set; }
    public TimeSpan TimeOfPossession { get; set; }
    public DriveResult Result { get; set; }
    public int StartingLOS { get; set; }
    public int TotalYards { get; set; }
    public int NumberOfPlays { get; set; }
    public IEnumerable<Play> Plays { get; set; }
        = new List<Play>();
    public bool LastDriveOfGame { get; set; } = false;

    public DriveRecord ToRecord()
    {
        return new DriveRecord(
            Id,
            GameId,
            OffenseId,
            DefenseId,
            TimeOfPossession,
            (int)Result,
            StartingLOS,
            TotalYards,
            NumberOfPlays,
            LastDriveOfGame);
    }
}
