using ConsoleRPG.Core;
using ConsoleRPG.Core.Factories;
using ConsoleRPG.Core.Models;

namespace ConsoleRPG;

public class Game
{
    private readonly ITextWriter _textWriter;

    private Location _currentLocation;

    public Game(ITextWriter textWriter)
    {
        _textWriter = textWriter;
        _currentLocation = WorldFactory.CreateWorld();
    }

    public void Start()
    {
        _textWriter.WriteLine($"Welcome to #location#. What do they call you?");

        Player? player = new();
        string? name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            _textWriter.WriteLine("Please enter a value.", ConsoleColor.Red);
            name = Console.ReadLine();
        }

        player.Name = name;
        _textWriter.WriteLine($"Welcome {name}!");

        TravelTo();

        Console.ReadLine();
    }

    private void TravelTo()
    {
        _textWriter.WriteLine($"You have arrived at {_currentLocation.Name}");
        _textWriter.WriteLine($"Where would you like to travel?");
        var availableLocations = _currentLocation.ConnectedLocations.Select(x => x.Name.ToLower());
        _textWriter.WriteOptions(availableLocations);
        var nextLocation = Console.ReadLine()?.ToLower();

        while (!availableLocations.Contains(nextLocation))
        {
            _textWriter.WriteLine("Choose an a valid location", ConsoleColor.Red);
            _textWriter.WriteOptions(availableLocations);
            nextLocation = Console.ReadLine();
        }

        _currentLocation = _currentLocation.ConnectedLocations
            .FirstOrDefault(x => x.Name.ToLower() == nextLocation)!;
        TravelTo();
    }
}
