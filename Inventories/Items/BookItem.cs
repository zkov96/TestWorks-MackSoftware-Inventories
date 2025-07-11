using Inventories.Interfaces;

namespace Inventories.Items;

public class BookItem : IItem
{
    public string Name { get; } = "Книга";
}