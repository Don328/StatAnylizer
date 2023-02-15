using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Interface.Display
{
    internal static class Teams
    {
        internal static void Show(IEnumerable<Team> teams)
        {
            Console.WriteLine();
            foreach (var team in teams)
            {
                Console.WriteLine(
                    $"[{team.Abbreviation}] [Id:{team.Id}] {team.City} {team.Name} ");
            }
        }
    }
}
