using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Core.Models.Weapons;
using ConsoleRPG.UI.CommandArguments;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Inventory;

internal class EquipCommand : Command
{
    public EquipCommand(ITextWriter textWriter, IServiceProvider serviceProvider) 
        : base(CommandNames.Equip, "Equip an item")
    {
        AddArgument(CommonArguments.Item);

        var gameSession = serviceProvider.GetService<GameSession>();

        this.SetHandler((string item) => {
            var inventory = gameSession!.CurrentPlayer.Inventory;
            var itemToEquip = inventory.FirstOrDefault(
                x => string.Equals(x.Name, item, StringComparison.InvariantCultureIgnoreCase));

            if (itemToEquip != null)
            {
                inventory.Add(gameSession!.CurrentPlayer!.Weapon!);
                inventory.Remove(itemToEquip);
                gameSession.CurrentPlayer.Weapon = (Weapon)itemToEquip;
                textWriter.WriteLine($"Equipped {item}");
                return;
            }

            textWriter.WriteLine($"No item to equip with name {item}");
        }, CommonArguments.Item);
    }
}
