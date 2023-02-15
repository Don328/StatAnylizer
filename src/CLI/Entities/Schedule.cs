using StatAnylizer.Core.Data.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Entities;

internal class Schedule : IEntity<ScheduleRecord>
{
    public int TeamId { get; set; }
    public int?[] Opponents { get; set; }
        = new int?[18];

    public ScheduleRecord ToRecord()
    {
        return new ScheduleRecord(TeamId, Opponents);
    }
}
