using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Combat;

internal class CombatCommand : Command
{
    public CombatCommand(ITextWriter textWriter, IServiceProvider serviceProvider, 
        RunCommand runCommand, AttackCommand attackCommand) 
        : base(CommandNames.Combat, "Actions you can perform when in Combat")
    {
        AddCommand(runCommand);
        AddCommand(attackCommand);

        this.SetHandler(() => {
            var gameSession = serviceProvider.GetService<GameSession>();

            if (!gameSession!.InCombat)
            {
                textWriter.WriteLine("You are not in combat!");
            }

            //Todo - Register command per available action in combat

            var monster = gameSession?.CurrentLocation.Monster;
        });
    }
}
