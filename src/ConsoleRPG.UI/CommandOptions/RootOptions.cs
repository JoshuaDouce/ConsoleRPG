using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

internal static class RootOptions
{
    public static Option<bool> StartOption { get; } = new Option<bool>(
            OptionNames.Start,
            description: "Starts the game",
            getDefaultValue: () => true);

    public static Option<string> InGameAction { get; } = new Option<string>(
            OptionNames.Action,
            description: "Action to execute in game");
}
