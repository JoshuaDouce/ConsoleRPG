using ConsoleRPG.Core.Models;
using ConsoleRPG.Core.Models.Weapons;

namespace ConsoleRPG.Core;

public class Player : Character
{
    public Player(string name) : base(name)
    {
    }

    public InventoryList Inventory { get; set; } = new InventoryList();

    public Weapon? EquippedWeapon { get; set; }

    public void Equip(string item)
    {
        var weaponToEquip = Inventory.Items
            .FirstOrDefault(x => string.Equals(x.Name, item, StringComparison.OrdinalIgnoreCase));

        if (weaponToEquip is null)
        {
            throw new ArgumentException($"No item in inventory with name {item}.");
        }

        if (weaponToEquip is not Weapon)
        {
            throw new ArgumentException($"{item} is not a Weapon.");
        }

        if (EquippedWeapon is not null)
        {
            Inventory.AddItem(EquippedWeapon);
        }

        Inventory.Items.Remove(weaponToEquip);
        EquippedWeapon = (Weapon)weaponToEquip;
    }

    public void UseWeapon()
    {
        EquippedWeapon?.UseWeapon();
    }
}
