using StatAnylizer.Core.Data.Access.JsonRepo;
using StatAnylizer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Interface.Prompt;

internal static class NewPenalty
{
    public static int Prompt(Penalty penalty)
    {
        PromptForDescription(penalty);
        PromptForYards(penalty);
        PromptForIsAutoFirst(penalty);
        penalty.Id = Save(penalty);
        return penalty.Id;
    }

    public static bool IsOnOffense()
    {
        Console.WriteLine();
        Console.WriteLine("Was the penalty against the offense? (y/n):");
        var response = Console.ReadLine()?? "";

        if (response.ToLower() == "y") return true;
        if (response.ToLower() == "n") return false;

        return IsOnOffense();
    }

    private static void PromptForDescription(Penalty penalty)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the penalty description. (ie: 'Holding' or 'Offsides')");
        var response = Console.ReadLine()?? string.Empty;

        if (string.IsNullOrEmpty(response)) PromptForDescription(penalty);

        penalty.Description = response;
    }
    
    private static void PromptForYards(Penalty penalty)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the number of yards for the penalty:");
        var response = Console.ReadLine();

        var success = Int32.TryParse(response, out int yards);
        if (!success)
        {
            Console.WriteLine("Penalty yardage must be a number");
            PromptForYards(penalty);
        }
        
        penalty.Yards = (uint)yards;
    }
    
    private static void PromptForIsAutoFirst(Penalty penalty)
    {
        Console.WriteLine("Does the penalty result in an automatic first down? (y/n)");
        var result = Console.ReadLine()?? string.Empty;
        if (result.ToLower() == "y") penalty.IsAutoFirst = true;
        else if (result.ToLower() == "n") penalty.IsAutoFirst = false;
        else PromptForIsAutoFirst(penalty);
    }

    private static int Save(Penalty penalty)
    {
        using var repo = new PenaltiesRepo();
        return repo.Save(penalty);
    }
}
