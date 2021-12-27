using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

internal static class CharacterOptions
{
    internal static Option<string> CharacterName { get; } = new Option<string>(
    OptionNames.CharacterName,
    description: "Your characters name");
}
