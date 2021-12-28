using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using ConsoleRPG.UI.CommandArguments;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Inventory;

internal class UnequipCommand : Command
{
    public UnequipCommand(ITextWriter textWriter, IServiceProvider serviceProvider) 
        : base(CommandNames.Unequip, "Unequip an item")
    {
        AddArgument(CommonArguments.Item);

        var gameSession = serviceProvider.GetService<GameSession>();

        this.SetHandler((string item) => {
            var inventory = gameSession!.CurrentPlayer.Inventory;

            if (string.Equals(
                item, gameSession.CurrentPlayer!.Weapon!.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                inventory.Add(gameSession!.CurrentPlayer!.Weapon!);
                gameSession.CurrentPlayer.Weapon = null;
                textWriter.WriteLine($"Unequipped {item}");
                return;
            }

            textWriter.WriteLine($"No item to equipped with name {item}");
        }, CommonArguments.Item);
    }
}
