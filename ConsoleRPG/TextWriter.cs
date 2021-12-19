using Microsoft.Extensions.Options;

namespace ConsoleRPG;

public class TextWriter : ITextWriter
{
    readonly GameSettings _gameSettings;

    public TextWriter(IOptions<GameSettings> gameSettings)
    {
        _gameSettings = gameSettings.Value;
    }

    public void Write(string text)
    {
        foreach (char character in text)
        {
            Console.Write(character);
            Thread.Sleep(_gameSettings.TextSpeed);
        }
    }
}
