using StatAnylizer.Core.Data.Access.Interfaces;
using StatAnylizer.Core.Data.Access.JsonRepo;
using StatAnylizer.Core.Data.Records;
using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core
{
    internal static class State
    {
        private static int? _season = null;
        private static bool _scheduleLoaded = false;

        internal static IEnumerable<Team> Teams
        { get; private set; }
            = new List<Team>();

        internal static int GetSeason() => _season ?? -1;
        internal static bool SeasonSet() => _season > 0;
        internal static bool ScheuleLoaded() => _scheduleLoaded;

        internal static void Init()
        {
            IAccessTeams teamsRepo = new TeamsRepo();
            Teams = teamsRepo.GetAll();
        }

        internal static void SetSeason(int season)
        {
            _season = season;

            IAccessSchedules schedulesRepo = new SchedulesRepo();
            var result = schedulesRepo.GetScheduleRecord(season);
            if (result != null)
            {
                foreach (var team in Teams)
                {
                    var schedule =
                        (from s in result
                         where s.TeamId == team.Id
                         select s).First();
                    team.AttatchSchedule(schedule.Opponents);
                }

                _scheduleLoaded = true;
            }
        }

        internal static void Reset()
        {
            _season = null;
            _scheduleLoaded = false;
            Teams = new List<Team>();
        }
    }
}
