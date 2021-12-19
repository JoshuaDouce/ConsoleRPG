using ConsoleRPG.Core;

namespace ConsoleRPG;

public class Game
{
    private readonly ITextWriter _textWriter;

    public Game(ITextWriter textWriter)
    {
        _textWriter = textWriter;
    }

    public void Start()
    {
        _textWriter.WriteLine($"Welcome to #location#. What do they call you?");

        Player? player = new();
        string? name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            _textWriter.WriteLine("Please enter a value.", ConsoleColor.Red);
            name = Console.ReadLine();
        }

        player.Name = name;
        _textWriter.WriteLine($"Welcome {name}!");

        Console.ReadLine();
    }
}
