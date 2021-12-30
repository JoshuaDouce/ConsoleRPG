using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using ConsoleRPG.UI.CommandArguments;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Inventory;

internal class EquipCommand : Command
{
    //TODO: this doesnt make sense to be a inventory sub command it should be a new Character command.
    public EquipCommand(ITextWriter textWriter, IServiceProvider serviceProvider) 
        : base(CommandNames.Equip, "Equip an item")
    {
        AddArgument(CommonArguments.Item);

        var gameSession = serviceProvider.GetService<GameSession>();

        this.SetHandler((string item) => {
            var player = gameSession!.CurrentPlayer;

            try
            {
                player.Equip(item);
                textWriter.WriteLine($"Equipped {item}.");
            }
            catch (Exception e)
            {
                textWriter.WriteLine(e.Message);
            }
        }, CommonArguments.Item);
    }
}
