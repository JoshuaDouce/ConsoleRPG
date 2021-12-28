namespace ConsoleRPG.Core.Models;

public abstract class Item
{
    public string Name { get; set; }

    public Item(string name)
    {
        Name = name;
    }
}
