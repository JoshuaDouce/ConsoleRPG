namespace ConsoleRPG.Core.Exceptions;

public class InCombatException : Exception
{
    public InCombatException(string? message) : base(message)
    {
    }
}
