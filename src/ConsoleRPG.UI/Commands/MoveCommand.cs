using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using ConsoleRPG.UI.Commands.Combat;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands;

internal class MoveCommand : Command
{
    public MoveCommand(ITextWriter textWriter, IServiceProvider serviceProvider) 
        : base(CommandNames.MoveCommand, "This command send you to the specified location")
    {
        var locationArgument = new Argument<string>("location");

        AddArgument(locationArgument);

        this.SetHandler((string location) =>
        {
            var gameSession = serviceProvider.GetService<GameSession>();

            try
            {
                gameSession?.TravelTo(location);
                textWriter?.WriteLine($"You have arrived at {location}");
            }
            catch (ArgumentException)
            {
                textWriter?.WriteLine($"{gameSession?.CurrentLocation.Name} " +
                    $"has no connected location called {location}.");
            }

            var monster = gameSession!.CurrentLocation.Monster;
            if (monster is not null)
            {
                textWriter?.WriteLine($"You have encountered a {monster.Name}");
                textWriter?.WriteLine($"You are in combat!");
            }

        }, locationArgument);
    }
}
