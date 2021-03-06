using ConsoleRPG.Interfaces;
using Microsoft.Extensions.Options;

namespace ConsoleRPG.Services;

public class TextWriter : ITextWriter
{
    private readonly GameSettings _gameSettings;

    public TextWriter(IOptions<GameSettings> gameSettings)
    {
        _gameSettings = gameSettings.Value;
    }

    public void Write(string text, ConsoleColor consoleColor = ConsoleColor.White)
    {
        Console.ForegroundColor = consoleColor;
        foreach (char character in text)
        {
            Console.Write(character);
            Thread.Sleep(_gameSettings.TextSpeed);
        }
        Console.ResetColor();
    }

    public void WriteLine(string text, ConsoleColor consoleColor = ConsoleColor.White)
    {
        Write($"{text}{Environment.NewLine}", consoleColor);
    }

    public void WriteOptions(IEnumerable<string> options)
    {
        foreach (string option in options) 
        {
            WriteLine(option);
        }
    }
}
