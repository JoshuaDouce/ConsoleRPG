using ConsoleRPG.Core;
using ConsoleRPG.Core.Factories;
using ConsoleRPG.Core.Models;

namespace ConsoleRPG;

public class GameSession
{
    public Location CurrentLocation;

    public Player CurrentPlayer;

    public GameSession()
    {
        CurrentLocation = WorldFactory.CreateWorld();
        CurrentPlayer = new Player
        {
            Name = "Brutus"
        };
    }

    public void TravelTo(string location)
    {
        var availableLocations = CurrentLocation.ConnectedLocations.Select(x => x.Name.ToLower());
        var attemptedLocation = CurrentLocation.ConnectedLocations
            .FirstOrDefault(x => x.Name.ToLower() == location);

        if (location == CurrentLocation.Name.ToLower())
            return;

        if (attemptedLocation == null)
            throw new ArgumentException($"Invalid location {location}");

        CurrentLocation = attemptedLocation;
    }
}
