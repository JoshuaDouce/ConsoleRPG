using System;
using System.Collections.Generic;
using ConsoleRPG.Core.Models;
using ConsoleRPG.Core.Models.Consumables;
using ConsoleRPG.Core.Models.Weapons;
using FluentAssertions;
using Xunit;

namespace ConsoleRPG.Core.Tests.Models;

public class PlayerTests
{
    private readonly Player TestPlayer;

    private readonly Sword EquippedSword = new("Sword");

    public PlayerTests()
    {
        TestPlayer = new Player
        {
            EquippedWeapon = EquippedSword
        };
        TestPlayer.Inventory.Items = new List<Item> {
            new Bow("Long Bow"),
            new Potion("Health Potion")
        };
    }

    [Fact]
    public void Equip_NullItem_ThrowsArgumentException()
    {
        // Act / Assert
        var result = Assert.Throws<ArgumentException>(() => TestPlayer.Equip("Not an Item"));
        Assert.Equal("No item in inventory with name Not an Item.", result.Message);
    }

    [Fact]
    public void Equip_InvalidItem_ThrowsArgumentException()
    {
        // Act / Assert
        var result = Assert.Throws<ArgumentException>(() => TestPlayer.Equip("Health Potion"));
        Assert.Equal("Health Potion is not a Weapon.", result.Message);
    }

    [Theory]
    [InlineData("Long Bow")]
    [InlineData("long bow")]
    public void Equip_ValidItem_ReplacesEquippedWeapon(string weapon)
    {
        // Act
        TestPlayer.Equip(weapon);

        // Assert
        TestPlayer.EquippedWeapon.Should().BeEquivalentTo(new Bow("Long Bow"));
        Assert.Equal(2, TestPlayer.Inventory.Items.Count);
        Assert.Contains(EquippedSword, TestPlayer.Inventory.Items);
    }

    [Fact]
    public void Equip_NoItemEquipped_EquipsNewWeapon()
    {
        // Arrange
        TestPlayer.EquippedWeapon = null;

        // Act
        TestPlayer.Equip("Long Bow");

        // Assert
        TestPlayer.EquippedWeapon.Should().BeEquivalentTo(new Bow("Long Bow"));
        Assert.Single(TestPlayer.Inventory.Items);
    }
}
