using StatAnylizer.Core.Data.Access.Interfaces;
using StatAnylizer.Core.Data.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Entities;

public class Player : IEntity<PlayerRecord>
{
    public Player(PlayerRecord record)
    {
        Id = record.Id;
        TeamId = record.TeamId;
        FirstName = record.FirstName;
        LastName = record.LastName;
    }

    public Player(int teamId)
    {
        TeamId = teamId;
    }

    public int Id { get; set; }
    public int TeamId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public PlayerRecord ToRecord()
    {
        return new PlayerRecord(
            Id,
            TeamId,
            FirstName,
            LastName);
    }
}
