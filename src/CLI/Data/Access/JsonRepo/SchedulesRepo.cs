using Newtonsoft.Json;
using StatAnylizer.Core.Data.Access.Interfaces;
using StatAnylizer.Core.Data.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.JsonRepo;

public class SchedulesRepo : IAccessSchedules
{
    private const string path =
        "./Data/Store/schedules.json";

    public IEnumerable<ScheduleRecord> GetScheduleRecord(int season)
    {
        using StreamReader reader = File.OpenText(path);
        var json = reader.ReadToEnd();
        List<SeasonScheduleRecord> records = 
            JsonConvert.DeserializeObject<List<SeasonScheduleRecord>>(json) 
            ?? new();

        var record = (from r in records
                        where r.Season == season
                        select r).FirstOrDefault();

        if (record == null)
        {
            return new List<ScheduleRecord>();
        }

        return record.Schedules;
    }
}
