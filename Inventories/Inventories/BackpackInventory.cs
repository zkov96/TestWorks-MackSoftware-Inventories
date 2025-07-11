using Inventories.Interfaces;

namespace Inventories.Inventories;

public class BackpackInventory : IInventory
{
    private readonly List<IItem> _items = new();
    public string Name { get; } = "Рюкзак";
    public int ItemsCount => _items.Count;

    public BackpackInventory()
    {
    }

    public BackpackInventory(params IItem[] items)
    {
        _items = items.ToList();
    }

    public BackpackInventory(IEnumerable<IItem> items)
    {
        foreach (var item in items)
        {
            PutItem(item);
        }
    }

    public IEnumerable<IItem> GetItems()
    {
        return _items;
    }

    public int PutItem(IItem item)
    {
        _items.Add(item);
        return _items.Count - 1;
    }

    public IItem PopItem(int index)
    {
        if (0 <= index && index < _items.Count)
        {
            return _items[index];
        }

        throw new ArgumentOutOfRangeException();
    }
}