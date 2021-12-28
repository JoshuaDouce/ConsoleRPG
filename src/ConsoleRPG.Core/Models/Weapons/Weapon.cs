using ConsoleRPG.Core.Interfaces;

namespace ConsoleRPG.Core.Models.Weapons;

public abstract class Weapon : Item, IWeaponBehaviour
{
    public Weapon(string name) : base(name)
    {
    }

    public abstract void UseWeapon();
}
