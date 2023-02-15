using Newtonsoft.Json;
using StatAnylizer.Core.Data.Access.Interfaces;
using StatAnylizer.Core.Data.Records;
using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.JsonRepo;

public class PenaltiesRepo : IAccessPenalties
{
    private const string path =
        "./Data/Store/penalties.json";

    public int Save(Penalty penalty)
    {
        var records = GetRecords().ToList();

        if (penalty.Id < 1)
            penalty.Id = IdTableRepo
                .GetNextPenaltyId();

        var record =
            (from r in records
             where r.Id == penalty.Id
             select r).FirstOrDefault();

        if (record != null) records.Remove(record);

        records.Add(penalty.ToRecord());

        var json = JsonConvert.SerializeObject(records);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);

        return penalty.Id;
    }

    public void Dispose(){}

    private static IEnumerable<PenaltyRecord> GetRecords()
    {
        using StreamReader reader = new StreamReader(path);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<PenaltyRecord>>(json) ?? new();
    }
}
