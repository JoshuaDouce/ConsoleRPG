using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.UI.CommandOptions;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands;

internal class InGameCommand : Command
{
    public InGameCommand(ITextWriter textWriter, IServiceProvider serviceProvider, MoveCommand moveCommand) 
        : base(CommandNames.InGame, "In game commands")
    {
        AddOption(InGameOptions.LocationList);
        AddCommand(moveCommand);

        var gameSession = serviceProvider.GetService<GameSession>();

        this.SetHandler((bool locationList, bool quit) =>
        {
            if (quit)
            {
                Environment.Exit(1);
            }

            if (locationList)
            {
                textWriter?.WriteLine($"You are currently at {gameSession?.CurrentLocation.Name}");
                textWriter?.WriteLine($"You can travel to:");
                textWriter?.WriteOptions(gameSession?.CurrentLocation.ConnectedLocations.Select(x => x.Name)!);
                return;
            }

            this.InvokeAsync("-h");
        }, InGameOptions.LocationList, CommonOptions.QuitOption);
    }
}
