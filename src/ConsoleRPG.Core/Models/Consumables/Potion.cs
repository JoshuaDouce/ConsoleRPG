using ConsoleRPG.Core.Interfaces;

namespace ConsoleRPG.Core.Models.Consumables;

public class Potion : Item, IConsumable
{
    public Potion(string name) : base(name)
    {
    }

    public void UseConsumable()
    {
        throw new NotImplementedException();
    }
}
