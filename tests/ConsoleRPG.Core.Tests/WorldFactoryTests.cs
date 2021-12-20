using System.Collections.Generic;
using System.Linq;
using ConsoleRPG.Core.Factories;
using ConsoleRPG.Core.Models;
using FluentAssertions;
using Xunit;

namespace ConsoleRPG.Core.Tests;

public class WorldFactoryTests
{
    [Fact]
    public void WorldFactory_AddLocations_CreatedTwoWayRelationship()
    {
        // Arrange 
        var initialLocation = new Location("Location 1");
        var locationToAdd = new Location("Location 2");

        // Act
        WorldFactory.AddLocations(initialLocation, new List<Location> { locationToAdd });

        // Assert
        locationToAdd.Should().BeEquivalentTo(initialLocation.ConnectedLocations[0]);
        initialLocation.Should().BeEquivalentTo(locationToAdd.ConnectedLocations[0]);
    }

    [Fact]
    public void WorldFactory_AddLocations_DoesNotAddNull()
    {
        // Arrange 
        var initialLocation = new Location("Location 1");

        // Act
        WorldFactory.AddLocations(initialLocation, new List<Location> { null });

        // Assert
        Assert.Empty(initialLocation.ConnectedLocations);
    }

    [Fact]
    public void WorldFactory_AddLocations_DoesNotCreateDuplicates()
    {
        // Arrange 
        var initialLocation = new Location("Location 1");
        var locationToAdd = new Location("Location 2");

        // Act
        WorldFactory.AddLocations(initialLocation, new List<Location> { locationToAdd, locationToAdd });

        // Assert
        Assert.Single(initialLocation.ConnectedLocations);
        Assert.Single(locationToAdd.ConnectedLocations);
    }

    [Fact]
    public void WorldFactory_CreateWorld_ReturnsWorldMap()
    {
        // Act
        var startingLocation = WorldFactory.CreateWorld();
        var forest = startingLocation.ConnectedLocations.Where(x => x.Name == "Forest").First();
        var dungeon = forest.ConnectedLocations.Where(x => x.Name == "Dungeon").First();

        // Assert
        Assert.Equal(2, startingLocation.ConnectedLocations.Count);
        Assert.Equal(2, forest.ConnectedLocations.Count);
        Assert.Single(dungeon.ConnectedLocations);
    }
}
