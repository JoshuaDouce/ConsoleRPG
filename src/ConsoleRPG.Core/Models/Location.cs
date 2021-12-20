namespace ConsoleRPG.Core.Models;

public class Location
{
    public string Name { get; set; }

    public List<Location> ConnectedLocations { get; set; }

    public Location(string name)
    {
        Name = name;
        ConnectedLocations = new List<Location>();
    }
}
