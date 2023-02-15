using StatAnylizer.Core.Entities;
using StatAnylizer.Core.Interface.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Interface.Prompt;

internal static class Menu
{
    internal static void Prompt()
    {
        State.Reset();
        State.Init();
        Console.WriteLine();
        Console.WriteLine("Enter one of the following commands for the specified results:");
        Console.WriteLine("(for example, 't' or 'teams' to display a list of all NFL Teams)");
        Console.WriteLine();
        Console.WriteLine("m       menu          - Show available commands (this list)");
        Console.WriteLine("t       teams         - Displays a list of NFL teams");
        Console.WriteLine("n       new-game      - Input new game data");
        Console.WriteLine("x       exit          - Exit");
        Console.WriteLine();

        Console.WriteLine("Enter command: ");
        var command = Console.ReadLine();
        Parse(command ?? "");
    }

    private static void Parse(string command)
    {
        switch (command.ToLower())
        {
            case "m":
                Prompt();
                break;
            case "menu":
                Prompt();
                break;
            case "t":
                ShowTeamsList();
                Prompt();
                break;
            case "teams":
                ShowTeamsList();
                Prompt();
                break;
            case "n":
                NewGame.Prompt();
                Prompt();
                break;
            case "new-game":
                NewGame.Prompt();
                Prompt();
                break;
            case "x":
                Exit();
                break;
            case "exit":
                Exit();
                break;
            default:
                Prompt();
                break;
        }
    }

    private static void ShowTeamsList()
    {
        Teams.Show(State.Teams);
        Console.WriteLine();
        Console.WriteLine(
            "Press any key to return to menu");
        Console.ReadLine();
    }

    private static void Exit()
    {
        Console.WriteLine();
        Console.WriteLine("Goodbye");
        Console.WriteLine();
    }
}