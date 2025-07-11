using Inventories.Interfaces;

namespace Inventories.Inventories;

public class TableInventory : IInventory
{
    private readonly HashSet<StackableItem> _items = new();
    public string Name { get; } = "Стол";
    public int ItemsCount => _items.Count;


    public TableInventory()
    {
    }

    public TableInventory(params IItem[] items)
    {
        foreach (var item in items)
        {
            PutItem(item);
        }
    }

    public TableInventory(IEnumerable<IItem> items)
    {
        foreach (var item in items)
        {
            PutItem(item);
        }
    }


    public int PutItem(IItem item)
    {
        return PutItem(item, _items.Count);
    }

    public IEnumerable<IItem> GetItems()
    {
        return _items;
    }

    public int PutItem(IItem item, int insertIndex)
    {
        var itemType = item.GetType();
        var sii = _items
            .Select((stackableItem, index) => ((StackableItem, int)?)(stackableItem, index))
            .FirstOrDefault(stackableItemIndex => stackableItemIndex?.Item1.ItemType == itemType);
        if (sii == null)
        {
            sii = (new StackableItem(item), _items.Count);
            _items.Add(sii.Value.Item1);
        }

        sii.Value.Item1.AddItem();

        return sii.Value.Item2;
    }

    public IItem PopItem(int index)
    {
        if (0 <= index && index < _items.Count)
        {
            var si = _items.Skip(index).First();
            var item = si.GetItem();

            if (si.IsEmpty)
            {
                _items.Remove(si);
            }

            return item;
        }

        throw new ArgumentOutOfRangeException();
    }

    private class StackableItem : IItem
    {
        public Type ItemType;
        public string Name => _count > 1 ? $"{_count}x {_name}" : _name;

        public bool IsEmpty => _count <= 0;

        private int _count = 0;
        private string _name;

        public StackableItem(IItem item)
        {
            _name = item.Name;
            ItemType = item.GetType();
        }

        public void AddItem()
        {
            AddItems(1);
        }

        public void AddItems(int appendCount)
        {
            _count += appendCount;
        }

        public IItem GetItem()
        {
            _count--;

            var item = (IItem)Activator.CreateInstance(ItemType)!;
            return item;
        }
    }
}