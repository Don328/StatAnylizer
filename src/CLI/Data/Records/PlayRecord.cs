using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Records;

public record PlayRecord(
    int Id,
    int GameId,
    int DriveId,
    int OffenseId,
    int DefenseId,

    int Down,
    int Distance,
    int LineOfScrimmage,
    TimeSpan GameClock,
    int Quarter,
    int PlayTypeIndex,
    int NetYards,
    TimeSpan TimeElapsed,
    
    bool FirstDownGained,
    bool IsTouchdown,
    bool EndOfDrive,
    bool EndOfQuarter,
    bool EndOfHalf,
    bool EndOf4th,
    bool EndOfGame,
    IEnumerable<int> TacklingPlayersId,
    int? PasserId = null,
    int? RecieverId = null,
    int? RunnerId = null,
    int? InterceptingPlayerId = null,
    int? RecoveringPlayerId = null,
    int? FumbleForcePlayerId = null,
    int? ReturnerId = null,
    int? KickerId = null,
    int? PunterId = null,
    int? PenaltyId = null,
    int? PreviousPlayId = null);
