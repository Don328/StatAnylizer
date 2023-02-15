using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.Interfaces;

public interface IAccessGames
{
    IEnumerable<Game> GetSeason();
    IEnumerable<Game> GetWeek(int week);
    IEnumerable<Game> GetTeamSeason(int teamId);
    Game? GetTeamWeek(int week, int teamId);
    Game? GetById(int id);
    int Save(Game game);
}
