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
        _textWriter.Write("Starting game....");

        Console.ReadLine();
    }
}
