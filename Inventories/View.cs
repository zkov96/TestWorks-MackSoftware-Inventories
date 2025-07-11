using Inventories.Interfaces;
using Inventories.Utils;

namespace Inventories;

public class View : IView
{
    public string Draw(IEnumerable<IInventory> inventories)
    {
        var strings = inventories.Select((inventory, inventoryIndex) =>
                {
                    var itemsStrings = inventory.GetItems().Select(item => item.Name);
                    itemsStrings = itemsStrings.Prepend(inventory.Name);
                    itemsStrings = itemsStrings.NormalizeStringsSize();
                    itemsStrings = itemsStrings.Select((s, i) => (i == 0 ? $"[{inventoryIndex}] " : $" {i - 1}: ") + s);
                    itemsStrings = itemsStrings.NormalizeStringsSize();

                    // itemsStrings = itemsStrings.NormalizeStringsSize();

                    return itemsStrings.ToArray();
                }
            )
            .ToArray();

        var maxStringsCount = strings.Max(strings1 => strings1.Length);
        var resultStrings = Enumerable.Range(0, maxStringsCount)
            .Select(i =>
                string.Join(" |  ",
                    strings.Select(strings1 => i >= strings1.Length
                        ? "".NormalizeSize(strings1.First().Length)
                        : strings1[i]
                    )
                )
            ).ToArray();

        return string.Join("\n", resultStrings);
    }
}