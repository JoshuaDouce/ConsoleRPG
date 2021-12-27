using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

internal class InGameOptions
{
    internal static Option<bool> LocationList { get; } = new Option<bool>(
        OptionNames.LocationList,
        description: "List all connected locations and your current location.",
        getDefaultValue: () => false);
}
