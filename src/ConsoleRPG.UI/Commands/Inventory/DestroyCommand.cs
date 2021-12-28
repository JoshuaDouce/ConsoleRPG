﻿using System.CommandLine;
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
            //TODO: The majority of thi slogic should be handled outside of the handler - Inventory Manager?
            //TODO: these three commands are very similiar can be refactored
            var inventory = gameSession!.CurrentPlayer.Inventory;
            var itemToDestroy = inventory.FirstOrDefault(
                x => string.Equals(x.Name, item, StringComparison.InvariantCultureIgnoreCase));

            if (itemToDestroy != null)
            {
                inventory.Remove(itemToDestroy);
                textWriter.WriteLine($"Destroyed {item}");
                return;
            }

            textWriter.WriteLine($"No item to destroy with name {item}");
        }, CommonArguments.Item);
    }
}
