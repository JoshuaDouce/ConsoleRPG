using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Combat;

internal class RunCommand : Command
{
    public RunCommand(ITextWriter textWriter, IServiceProvider serviceProvider) : 
        base(CommandNames.Run, "Run from combat")
    {
        this.SetHandler(() =>
        {
            var gameSession = serviceProvider.GetService<GameSession>();

            if (!gameSession!.InCombat)
            {
                textWriter.WriteLine("You are not in combat!");
                return;
            }

            //This could be % based result based on player vs enemy stats.
            //But this logic should not live here!
            gameSession!.InCombat = false;
            textWriter.WriteLine("You succesfully fled from combat.");
        });
    }
}
