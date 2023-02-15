using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.Interfaces;

public interface IAccessPlayers
{
    IEnumerable<Player> GetAll();
    IEnumerable<Player> GetByTeam(int  teamId);
    int Save(Player player);
}
