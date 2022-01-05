using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Combat;

internal class CombatCommand : Command
{
    public CombatCommand(ITextWriter textWriter, IServiceProvider serviceProvider) 
        : base(CommandNames.Combat, "Actions you can perform when in Combat")
    {
        var gameSession = serviceProvider.GetService<GameSession>();

        this.SetHandler(() => {
            if (!gameSession!.InCombat)
            {
                textWriter.WriteLine("You are not in combat!");
            }

            //Todo - Register command per available action in combat

            var monster = gameSession?.CurrentLocation.Monster;
        });
    }
}
