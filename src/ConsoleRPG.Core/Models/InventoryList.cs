namespace ConsoleRPG.Core.Models;

public class InventoryList
{
    public List<Item> Items { get; set; } = new List<Item>();

    public void AddItem(Item item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        Items.Add(item);
    }

    public void DestroyItem(string itemName)
    {
        var itemToDestory = Items
            .FirstOrDefault(x => string.Equals(x.Name, itemName, StringComparison.OrdinalIgnoreCase));

        if (itemToDestory == null)
            throw new ArgumentException($"No item do detroy with name {itemName}");

        Items.Remove(itemToDestory);
    }
}
