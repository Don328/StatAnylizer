using StatAnylizer.Core.Data.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.Interfaces;

public interface IAccessSchedules
{
    public IEnumerable<ScheduleRecord> GetScheduleRecord(int season);
}
