using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

internal class InventoryOptions
{
    internal static Option<bool> InventoryList { get; } = new Option<bool>(
        OptionNames.List,
        description: "List all items in your inventory",
        getDefaultValue: () => false);

    internal static Option<bool> EquippedList { get; } = new Option<bool>(
        OptionNames.EquippedList,
        description: "List all equiped items",
        getDefaultValue: () => false);
}
