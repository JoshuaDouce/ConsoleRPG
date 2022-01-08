using ConsoleRPG.Core.Factories;
using ConsoleRPG.Core.Models;
using ConsoleRPG.Core.Models.Consumables;
using ConsoleRPG.Core.Models.Weapons;

namespace ConsoleRPG.Core;

public class GameSession
{
    public Location CurrentLocation;

    public Player CurrentPlayer;

    public bool InCombat = false;

    public GameSession()
    {
        CurrentLocation = WorldFactory.CreateWorld();
        CurrentPlayer = new Player("")
        {
            EquippedWeapon = new Sword("Sword"),
            Health = 10
        };
        CurrentPlayer.Inventory.Items = new List<Item> {
            new Bow("Long Bow"),
            new Potion("Health Potion")
        };
    }

    public void TravelTo(string location)
    {
        if (InCombat)
        {
            throw new Exception("Cannot travel when you are in combat!");
        }

        var availableLocations = CurrentLocation.ConnectedLocations.Select(x => x.Name.ToLower());
        var attemptedLocation = CurrentLocation.ConnectedLocations
            .FirstOrDefault(x => string.Equals(location, x.Name, StringComparison.InvariantCultureIgnoreCase));

        if (string.Equals(location, CurrentLocation.Name, StringComparison.InvariantCultureIgnoreCase))
            return;

        if (attemptedLocation == null)
            throw new ArgumentException($"Invalid location {location}");

        CurrentLocation = attemptedLocation;

        if (CurrentLocation.Monster is not null)
        {
            InCombat = true;
        }
    }
}
