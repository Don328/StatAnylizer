using StatAnylizer.Core.Data.Access.JsonRepo;
using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Interface.Prompt
{
    internal static class NewPlayer
    {
        internal static void Prompt(Player player)
        {
            PromptForFirstName(player);
            PromptForLastName(player);

            var playersRepo = new PlayersRepo();
            playersRepo.Save(player);
        }

        private static void PromptForFirstName(Player player)
        {
            Console.WriteLine();
            Console.WriteLine("Enter the players first name:");
            var response = Console.ReadLine()?? "";

            // Add Validation and auto formatting

            player.FirstName = response;

        }

        private static void PromptForLastName(Player player)
        {
            Console.WriteLine();
            Console.WriteLine("Enter the players last name:");
            var response = Console.ReadLine() ?? "";

            // Add Validation and auto formatting

            player.LastName = response;
        }
    }
}
