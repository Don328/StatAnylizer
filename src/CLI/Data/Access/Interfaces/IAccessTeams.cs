using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.Interfaces
{
    public interface IAccessTeams
    {
        public IEnumerable<Team> GetAll();
        public Team GetById(int id);
    }
}
