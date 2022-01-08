namespace ConsoleRPG.Core.Models;

public abstract class Character
{
    public string Name { get; set; } = default!;

    public int Health { get; set; } = 10;

    public int Attack { get; set; } = 1;

    public int Defense { get; set; }

    public Character(string name)
    {
        Name = name;
    }
}
