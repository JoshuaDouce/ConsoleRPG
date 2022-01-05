namespace ConsoleRPG.Core.Models;

public class Location
{
    public string Name { get; set; }

    public List<Location> ConnectedLocations { get; set; }

    public Monster? Monster { get; set; }

    public Location(string name)
    {
        Name = name;
        ConnectedLocations = new List<Location>();
    }

    public void AddMonster(Monster monster)
    {
        Monster = monster;
    }
}
