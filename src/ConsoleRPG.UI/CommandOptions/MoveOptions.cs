using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

public static class MoveOptions
{
    public static Option<string> LocationOption { get; } = new Option<string>(
        "location",
        description: "The location to travel too");
}
