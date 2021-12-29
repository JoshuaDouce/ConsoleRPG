using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using ConsoleRPG.UI.CommandArguments;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Inventory;

internal class DestroyCommand : Command
{
    public DestroyCommand(ITextWriter textWriter, IServiceProvider serviceProvider) 
        : base(CommandNames.Destroy, "Destroy an Item")
    {
        AddArgument(CommonArguments.Item);

        var gameSession = serviceProvider.GetService<GameSession>();

        this.SetHandler((string item) => {
            var inventory = gameSession!.CurrentPlayer.Inventory;

            try
            {
                inventory.DestroyItem(item);
                textWriter.WriteLine($"Destroyed {item}");
            }
            catch (Exception e)
            {

                textWriter.WriteLine(e.Message);
            }            
        }, CommonArguments.Item);
    }
}
