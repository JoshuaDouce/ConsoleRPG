namespace ConsoleRPG;

public interface ITextWriter
{
    public void Write(string text, ConsoleColor consoleColor = ConsoleColor.White);

    public void WriteLine(string text, ConsoleColor consoleColor = ConsoleColor.White);

    void WriteOptions(IEnumerable<string> options);
}
