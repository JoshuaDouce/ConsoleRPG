using System.Collections.Generic;
using ConsoleRPG.Core.Models;
using ConsoleRPG.Core.Models.Consumables;
using ConsoleRPG.Core.Models.Weapons;
using FluentAssertions;
using Xunit;

namespace ConsoleRPG.Core.Tests;

public class GameSessionTests
{
    [Fact]
    public void Constructor_SetsInitialState()
    {
        // Arrange
        var player = new Player
        {
            EquippedWeapon = new Sword("Sword")
        };

        player.Inventory.Items = new List<Item> {
            new Bow("Long Bow"),
            new Potion("Health Potion")
        };

        // Act
        var gameSession = new GameSession();

        // Assert
        gameSession.CurrentPlayer.Should().BeEquivalentTo(player);
        Assert.Equal("Town", gameSession.CurrentLocation.Name);
    }

    [Fact]
    public void TravelTo_ValidLocation_UpdatesCurrentLocation()
    {
        // Arrange
        var gameSession = new GameSession();
        var travelLocation = "Swamp";

        // Act
        gameSession.TravelTo(travelLocation);

        // Assert
        Assert.Equal(travelLocation, gameSession.CurrentLocation.Name);
    }
}
