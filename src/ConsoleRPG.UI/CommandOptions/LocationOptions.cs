using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

internal static class LocationOptions
{
    internal static Option<string> LocationOption { get; } = new Option<string>(
        OptionNames.Location,
        description: "The location to travel too");
}
