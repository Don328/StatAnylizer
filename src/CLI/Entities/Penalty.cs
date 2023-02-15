using StatAnylizer.Core.Data.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Entities;

public class Penalty : IEntity<PenaltyRecord>
{
    public Penalty(PenaltyRecord record)
    {
        Id = record.Id;
        GameId = record.GameId;
        DriveId = record.DriveId;
        PlayId = record.PlayId;
        TeamId = record.TeamId;
        AgainstOffense = record.AgainstOffense;
        IsAutoFirst = record.IsAutoFirst;
        Description = record.Description;
        PlayerId = record.PlayerId;
        Yards = record.Yards;
    }

    public Penalty(
        int gameId,
        int driveId,
        int playId)
    {
        Id = -1;
        GameId = gameId;
        DriveId = driveId;
        PlayId = playId;
    }

    public int Id { get; set; }
    public int GameId { get; set; }
    public int DriveId { get; set; }
    public int PlayId { get; set; }
    public int TeamId { get; set; }
    public bool AgainstOffense { get; set; }
    public bool IsAutoFirst { get; set; }
    public string Description { get; set; } = string.Empty;
    public int? PlayerId { get; set; }
    public uint Yards { get; set; }

    public PenaltyRecord ToRecord()
    {
        return new PenaltyRecord(
            Id,
            GameId,
            DriveId,
            PlayId,
            TeamId,
            AgainstOffense,
            IsAutoFirst,
            Description,
            PlayerId,
            Yards);
    }
}
