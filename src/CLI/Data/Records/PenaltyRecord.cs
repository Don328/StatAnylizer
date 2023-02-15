using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Records;

public record PenaltyRecord(
    int Id,
    int GameId,
    int DriveId,
    int PlayId,
    int TeamId,
    bool AgainstOffense,
    bool IsAutoFirst,
    string Description,
    int? PlayerId,
    uint Yards);
