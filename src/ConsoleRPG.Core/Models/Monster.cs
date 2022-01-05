namespace ConsoleRPG.Core.Models;

public class Monster
{
    public string Name { get; set; }

    public int Health { get; set; }

    public Monster(string name)
    {
        Name = name;
    }
}
