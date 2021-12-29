using System;
using System.Collections.Generic;
using ConsoleRPG.Core.Models;
using ConsoleRPG.Core.Models.Consumables;
using ConsoleRPG.Core.Models.Weapons;
using Xunit;

namespace ConsoleRPG.Core.Tests.Models;

public class InventoryListTests
{
    private readonly InventoryList Sut = new() 
    { 
        Items = new List<Item> 
        {
            new Bow("Long Bow"),
            new Potion("Health Potion")
        }
    };

    [Fact]
    public void AddItem_NullItem_ThrowsException()
    {
        //Act / Assert
        Assert.Throws<ArgumentNullException>(() => Sut.AddItem(null));
    }

    [Fact]
    public void AddItem_ValidItem_AddsItem()
    {
        // Arrange
        var newItem = new Sword("Shiny Sword");

        // Act
        Sut.AddItem(newItem);

        // Assert
        Assert.Equal(3, Sut.Items.Count);
        Assert.Contains(newItem, Sut.Items);
    }

    [Fact]
    public void DestroyItem_NoSuchItem_ThrowsException()
    {
        // Act / Assert
        var result = Assert.Throws<ArgumentException>(() => Sut.DestroyItem("random item"));
        Assert.Equal("No item do detroy with name random item", result.Message);
    }

    [Theory]
    [InlineData("Long Bow")]
    [InlineData("long bow")]
    public void DestroyItem_ValidItem_RemovesItem(string itemName)
    {
        // Act
        Sut.DestroyItem(itemName);

        // Assert
        Assert.Single(Sut.Items);
    }
}
