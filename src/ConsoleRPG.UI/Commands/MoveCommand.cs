using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.UI.CommandOptions;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands;

internal class MoveCommand : Command
{
    public MoveCommand(ITextWriter textWriter, IServiceProvider serviceProvider) 
        : base(CommandNames.MoveCommand, "This command send you to the specified location")
    {
        AddOption(LocationOptions.LocationOption);

        this.SetHandler((string location) =>
        {
            var gameSession = serviceProvider.GetService<GameSession>();

            try
            {
                gameSession?.TravelTo(location);
                textWriter?.WriteLine($"You have arrived at {gameSession?.CurrentLocation.Name}");
            }
            catch (ArgumentException)
            {
                textWriter?.WriteLine($"{gameSession?.CurrentLocation.Name} " +
                    $"has no connected location called {location}.");
            }
        }, LocationOptions.LocationOption);
    }
}
