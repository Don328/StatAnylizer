using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Records;

public record DriveRecord(
    int Id,
    int GameId,
    int OffenseId,
    int DefenseId,
    TimeSpan TimeOfPossession,
    int ResultIndex,
    int StartingLOS,
    int TotalYards,
    int NumberOfPlays,
    bool LastDriveOfGame);
