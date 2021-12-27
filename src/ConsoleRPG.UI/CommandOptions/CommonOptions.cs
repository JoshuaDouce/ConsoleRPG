using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

internal static class CommonOptions
{
    internal static Option<bool> QuitOption { get; } = new Option<bool>(
            OptionNames.Quit,
            description: "Exits the game (Without saving)",
            getDefaultValue: () => false);
}
