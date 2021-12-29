using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using ConsoleRPG.UI.CommandOptions;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Inventory;

internal class InventoryCommand : Command
{
    public InventoryCommand(IServiceProvider serviceProvider, ITextWriter textWriter, 
        DestroyCommand destroyCommand, EquipCommand equipCommand) 
        : base(CommandNames.Inventory, 
        "Commands for managing inventory and equipped items")
    {
        AddOption(InventoryOptions.InventoryList);
        AddOption(InventoryOptions.EquippedList);
        AddCommand(destroyCommand);
        AddCommand(equipCommand);

        this.SetHandler((bool inventoryList, bool equippedList) => {
            var gameSession = serviceProvider.GetService<GameSession>();

            if (inventoryList)
            {
                textWriter.WriteLine("Items currently in your inventory:");
                textWriter.WriteOptions(gameSession!.CurrentPlayer.Inventory.Items.Select(x => x.Name));
            }

            if (equippedList)
            {
                var weapon = gameSession!.CurrentPlayer!.EquippedWeapon;
                if (weapon is null)
                {
                    textWriter.WriteLine("You have nothing equipped");
                }
                else
                {
                    textWriter.WriteLine("Items currently equipped:");
                    textWriter.WriteLine($"{weapon.Name}");
                }
            }

        }, InventoryOptions.InventoryList, InventoryOptions.EquippedList);
    }
}
