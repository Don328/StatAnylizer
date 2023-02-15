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

public class TeamsRepo : IAccessTeams
{
    private const string path =
        "./Data/Store/teams.json";

    public IEnumerable<Team> GetAll()
    {
        try
        {
            using StreamReader reader = File.OpenText(path);
            var json = reader.ReadToEnd();
            List<TeamRecord> records = JsonConvert.DeserializeObject<List<TeamRecord>>(json) ?? new();
            List<Team> teams = new List<Team>();
            foreach( var record in records)
            {
                var team = new Team(record);
                teams.Add(team);
            }

            return teams;
        }
        catch (Exception e)
        {
            Console.WriteLine();
            Console.WriteLine(e.Message);
            Console.WriteLine("Error reading teams list from file.");
            Console.WriteLine();
            return new List<Team>();
        }
    }

    public Team GetById(int id)
    {
        return (from t in GetAll().ToList()
                where t.Id == id
                select t).First();
    }
}
