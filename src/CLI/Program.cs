using StatAnylizer.Core;
using StatAnylizer.Core.Entities;
using StatAnylizer.Core.Interface.Prompt;
using System.Text;

namespace StatAnylizer;

public static class Program
{
    public static void Main(string[] args)
    {
        State.Init();
        Menu.Prompt();
    }
}