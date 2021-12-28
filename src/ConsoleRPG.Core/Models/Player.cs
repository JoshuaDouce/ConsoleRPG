using ConsoleRPG.Core.Models;
using ConsoleRPG.Core.Models.Weapons;

namespace ConsoleRPG.Core;

public class Player
{
    public string Name { get; set; } = null!;

    public List<Item> Inventory { get; set; } = new List<Item>();

    public Weapon? Weapon { get; set; }
}
