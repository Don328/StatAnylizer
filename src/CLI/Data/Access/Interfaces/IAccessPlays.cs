using StatAnylizer.Core.Entities;
using StatAnylizer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Access.Interfaces;

public interface IAccessPlays : IDisposable
{
    IEnumerable<Play> GetByGame(int gameId);
    IEnumerable<Play> GetByDrive(int driveId);
    IEnumerable<Play> GetByTeam(int teamId);
    int Save(Play play);
}
