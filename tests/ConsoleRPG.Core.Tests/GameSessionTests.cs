using FluentAssertions;
using Xunit;

namespace ConsoleRPG.Core.Tests;

public class GameSessionTests
{
    [Fact]
    public void Constructor_SetsInitialState()
    {
        // Act
        var gameSession = new GameSession();

        // Assert
        gameSession.CurrentPlayer.Should().BeEquivalentTo(new Player());
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
