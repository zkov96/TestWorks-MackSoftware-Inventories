using Inventories;
using Inventories.Interfaces;
using Inventories.Inventories;
using Inventories.Items;

public static class Program
{
    private static CancellationTokenSource _cts = new();

    public static void Main(string[] args)
    {
        Console.CancelKeyPress += ConsoleOnCancelKeyPress;

        var inventoryManager = new InventoryManager();

        inventoryManager.AddInventory(new BackpackInventory());
        inventoryManager.AddInventory(
            new TableInventory(
                Enumerable.Empty<IItem>()
                    .Append(new AmuItem())
                    .Concat(Enumerable.Repeat<object>(null!, 33).Select(_ => new MoneyItem()))
                    .Append(new BookItem())
            )
        );

        Task.WaitAll(inventoryManager.Start(_cts.Token));
    }

    private static void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        _cts.Cancel();
    }
}