using StatAnylizer.Core.Data.Records;
using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Entities;

public class Play : IEntity<PlayRecord>
{
    public Play(PlayRecord record)
    {
        Id = record.Id;
        GameId = record.GameId;
        DriveId = record.DriveId;
        OffenseId = record.OffenseId;
        DefenseId = record.DefenseId;
        Down = record.Down;
        Distance = record.Distance;
        LineOfScrimmage = record.LineOfScrimmage;
        GameClock = record.GameClock;
        Quarter = record.Quarter;
        PlayType = (PlayType)record.PlayTypeIndex;
        NetYards = record.NetYards;
        TimeElapsed = record.TimeElapsed;
        FirstDownGained = record.FirstDownGained;
        IsTouchdown = record.IsTouchdown;
        EndOfDrive = record.EndOfDrive;
        EndOfQuarter = record.EndOfQuarter;
        EndOfHalf = record.EndOfHalf;
        EndOf4th = record.EndOf4th;
        EndOfGame = record.EndOfGame;
        TacklingPlayersId = record.TacklingPlayersId;
        PasserId = record.PasserId;
        RecieverId = record.RecieverId;
        RunnerId = record.RunnerId;
        InterceptingPlayerId = record.InterceptingPlayerId;
        RecoveringPlayerId = record.RecoveringPlayerId;
        FumbleForcePlayerId = record.FumbleForcePlayerId;
        ReturnerId = record.ReturnerId;
        KickerId = record.KickerId;
        PunterId = record.PunterId;
        PenaltyId = record.PenaltyId;
        PreviousPlayId = record.PreviousPlayId;
    }

    public Play(Drive drive)
    {
        GameId = drive.GameId;
        DriveId = drive.Id;
        OffenseId = drive.OffenseId;
        DefenseId = drive.DefenseId;
    }

    public int Id { get; set; }
    public int GameId { get; set; }
    public int DriveId { get; set; }
    public int OffenseId { get; set; }
    public int DefenseId { get; set; }
    public int Down { get; set; }
    public int Distance { get; set; }
    public int LineOfScrimmage { get; set; }
    public TimeSpan GameClock { get; set; }
    public int Quarter { get; set; } 
    public PlayType PlayType { get; set; }
    public int NetYards { get; set; }
    public TimeSpan TimeElapsed { get; set; }
    public bool FirstDownGained { get; set; }
    public bool IsTouchdown { get; set; }
    public bool EndOfDrive { get; set; }
    public bool EndOfQuarter { get; set; }
    public bool EndOfHalf { get; set; }
    public bool EndOf4th { get; set; }
    public bool EndOfGame { get; set; }
    public IEnumerable<int> TacklingPlayersId { get; set; } = new int[0];
    public int? PasserId { get; set; }
    public int? RecieverId { get; set; }
    public int? RunnerId { get; set; }
    public int? InterceptingPlayerId { get; set; }
    public int? RecoveringPlayerId { get; set; }
    public int? FumbleForcePlayerId { get; set; }
    public int? ReturnerId { get; set; }
    public int? KickerId { get; set; }
    public int? PunterId { get; set; }
    public int? PenaltyId { get; set; }
    public int? PreviousPlayId { get; set; }

    public PlayRecord ToRecord()
    {
        return new PlayRecord(
            Id: Id,
            GameId: GameId,
            DriveId: DriveId,
            OffenseId: OffenseId,
            DefenseId: DefenseId,
            Down: Down,
            Distance: Distance,
            LineOfScrimmage: LineOfScrimmage,
            GameClock: GameClock,
            Quarter: Quarter,
            PlayTypeIndex: (int)PlayType,
            NetYards: NetYards,
            TimeElapsed: TimeElapsed,
            FirstDownGained: FirstDownGained,
            IsTouchdown: IsTouchdown,
            EndOfDrive: EndOfDrive, 
            EndOfQuarter: EndOfQuarter,
            EndOfHalf: EndOfHalf,
            EndOf4th: EndOf4th,
            EndOfGame: EndOfGame,
            TacklingPlayersId: TacklingPlayersId,
            PasserId: PasserId,
            RecieverId: RecieverId,
            RunnerId: RunnerId,
            InterceptingPlayerId: InterceptingPlayerId,
            RecoveringPlayerId: RecoveringPlayerId,
            FumbleForcePlayerId: FumbleForcePlayerId,
            ReturnerId: ReturnerId,
            KickerId: KickerId,
            PunterId: PunterId,
            PenaltyId: PenaltyId,
            PreviousPlayId: PreviousPlayId);
    }
}
