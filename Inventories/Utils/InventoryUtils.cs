namespace Inventories.Utils;

public static class InventoryUtils
{
    // public static IEnumerable<string> GetView(this IInventory inventory)
    // {
    //     List<string> strings = [inventory.Name];
    //
    //     strings.AddRange(inventory.GetItems().Select(item => item.Name));
    //
    //     return strings;
    // }
    
    public static IEnumerable<string> NormalizeStringsSize(this IEnumerable<string> stringsE)
    {
        var strings = stringsE.ToArray();
        if (strings.Length <= 0)
        {
            return [];
        }
        
        var maxStringSize = strings.Max(s => s.Length);
        return strings.Select(s => s.NormalizeSize(maxStringSize));
    }
}