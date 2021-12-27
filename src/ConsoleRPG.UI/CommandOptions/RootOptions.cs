using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

internal static class RootOptions
{
    internal static Option<bool> StartOption { get; } = new Option<bool>(
            OptionNames.Start,
            description: "Starts the game",
            getDefaultValue: () => true);
}
