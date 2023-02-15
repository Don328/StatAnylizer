using StatAnylizer.Core.Data.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Entities
{
    public class Game : IEntity<GameRecord>
    {
        public Game(GameRecord record)
        {
            Id = record.Id;
            Season = record.Season;
            Week = record.Week;
            HomeId = record.HomeId;
            AwayId = record.AwayId;
            IsFinal = record.IsFinal;
        }

        public Game(
            int season, 
            int week, 
            int homeId, 
            int awayId)
        {
            Season = season;
            Week = week;
            HomeId = homeId;
            AwayId = awayId;
        }

        public int Id { get; set; }
        public int Season { get; set; }
        public int Week { get; set; }
        public int HomeId { get; set; }
        public int AwayId { get; set; }
        public bool IsFinal { get; set; } = false;
        public IEnumerable<Drive> Drives { get; set; }
            = new List<Drive>();

        public GameRecord ToRecord()
        {
            return new GameRecord(
                Id,
                Season,
                Week,
                HomeId,
                AwayId,
                IsFinal);
        }
    }
}
