using ConsoleRPG.Core.Factories;
using ConsoleRPG.Core.Models;

namespace ConsoleRPG.Core;

public class GameSession
{
    public Location CurrentLocation;

    public Player CurrentPlayer;

    public GameSession()
    {
        CurrentLocation = WorldFactory.CreateWorld();
        CurrentPlayer = new Player();
    }

    public void TravelTo(string location)
    {
        var availableLocations = CurrentLocation.ConnectedLocations.Select(x => x.Name.ToLower());
        var attemptedLocation = CurrentLocation.ConnectedLocations
            .FirstOrDefault(x => string.Equals(location, x.Name, StringComparison.InvariantCultureIgnoreCase));

        if (string.Equals(location, CurrentLocation.Name, StringComparison.InvariantCultureIgnoreCase))
            return;

        if (attemptedLocation == null)
            throw new ArgumentException($"Invalid location {location}");

        CurrentLocation = attemptedLocation;
    }
}
