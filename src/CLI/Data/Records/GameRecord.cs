using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Records;

public record GameRecord(
    int Id,
    int Season,
    int Week,
    int HomeId,
    int AwayId,
    bool IsFinal);

