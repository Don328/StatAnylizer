using StatAnylizer.Core.Data.Records;
using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Entities
{
    public class Team : IEntity<TeamRecord>
    {
        public Team(TeamRecord record)
        {
            Id = record.Id;
            City = record.City;
            Name = record.Name;
            Abbreviation = record.Abbreviation;
            Conference = (Conference)record.Conference_Index;
            Division = (Division)record.Division_Index;
        }

        public int Id { get; }
        public string City { get; } = string.Empty;
        public string Name { get; } = string.Empty;
        public string Abbreviation { get; } = string.Empty;
        public Conference Conference { get; }
        public Division Division { get; }
        public int?[] Schedule { get; private set; } = new int?[18];

        public void AttatchSchedule(int?[] schedule)
        {
            Schedule = schedule;
        }

        public TeamRecord ToRecord()
        {
            return new TeamRecord(
                Id,
                City,
                Name,
                Abbreviation,
                (int)Conference,
                (int)Division);
        }
    }
}
