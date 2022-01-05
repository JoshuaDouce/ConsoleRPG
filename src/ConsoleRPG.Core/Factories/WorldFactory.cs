using ConsoleRPG.Core.Models;

namespace ConsoleRPG.Core.Factories;

public static class WorldFactory
{
    /// <summary>
    /// Builds the world
    /// </summary>
    /// <returns>The starting Location</returns>
    public static Location CreateWorld()
    {
        var town = new Location("Town");
        var forest = new Location("Forest");
        var swamp = new Location("Swamp");
        var dungeon = new Location("Dungeon");
        forest.AddMonster(new Monster("Spider"));
        swamp.AddMonster(new Monster("Bog Dweller"));
        dungeon.AddMonster(new Monster("Skeleton"));

        AddLocations(town, new List<Location> { forest, swamp });
        AddLocations(forest, new List<Location> { dungeon });

        return town;
    }

    private static void AddLocation(Location initialLocation, Location locationToAdd)
    {
        if (initialLocation is null || locationToAdd is null)
        {
            return;
        }

        if (!initialLocation.ConnectedLocations.Contains(locationToAdd))
        {
            initialLocation.ConnectedLocations.Add(locationToAdd);
        }

        if (!locationToAdd.ConnectedLocations.Contains(initialLocation))
        {
            locationToAdd.ConnectedLocations.Add(initialLocation);
        }
    }

    public static void AddLocations(Location baseLocation, IEnumerable<Location> locations)
    {
        foreach (var location in locations)
        {
            AddLocation(baseLocation, location);
        }
    }
}
