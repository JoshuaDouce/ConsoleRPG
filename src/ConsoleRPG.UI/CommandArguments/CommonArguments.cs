using System.CommandLine;

namespace ConsoleRPG.UI.CommandArguments;

internal class CommonArguments
{
    internal static Argument<string> Item { get; } = new Argument<string>("item");
}
