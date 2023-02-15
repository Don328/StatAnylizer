using StatAnylizer.Core.Data.Records;
using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.Interfaces;

public interface IAccessDrives
{
    IEnumerable<Drive> GetGame(int gameId);
    int Save(Drive drive);
}
