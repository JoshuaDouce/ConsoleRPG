using System.CommandLine;

namespace ConsoleRPG.UI.CommandOptions;

internal static class CharacterOptions
{
    internal static Option<string> CharacterName { get; } = new Option<string>(
        aliases: OptionNames.CharacterNameAliases,
        description: "Your characters name");
}
