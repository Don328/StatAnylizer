using Newtonsoft.Json;
using StatAnylizer.Core.Data.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.JsonRepo;

internal static class IdTableRepo
{
    private const string path =
    "./Data/Store/id_table.json";

    internal static int GetNextDriveId()
    {
        var table = Get();
        table.Drives++;
        Save(table);
        return table.Drives;
    }

    internal static int GetNextGameId()
    {
        var table = Get();
        table.Games++;
        Save(table);
        return table.Games;
    }

    internal static int GetNextPlayerId()
    {
        var table = Get();
        table.Players++;
        Save(table);
        return table.Players;
    }

    internal static int GetNextPlayId()
    {
        var table = Get();
        table.Plays++;
        Save(table);
        return table.Plays;
    }

    internal static int GetNextPenaltyId()
    {
        var table = Get();
        table.Penalties++;
        Save(table);
        return table.Penalties;
    }

    private static IdTable Get()
    {
        try
        {
            using StreamReader reader = File.OpenText(path);
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<IdTable>(json) 
                ?? throw new Exception(message: "Error fetching Id Table data");
        }
        catch (Exception e)
        {
            Console.WriteLine();
            Console.WriteLine(e.Message);
            Console.WriteLine();
            return new IdTable();
        }

    }

    private static void Save(IdTable table)
    {
        var json = JsonConvert.SerializeObject(table);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
    }
}