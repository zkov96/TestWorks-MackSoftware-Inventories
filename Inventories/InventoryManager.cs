using System.Collections.Concurrent;
using Inventories.Interfaces;

namespace Inventories;

public class InventoryManager
{
    private List<IInventory> _inventories = new();

    private IView _currentView = new View();

    private volatile ConcurrentBag<string> _errors = new();

    private volatile bool _needRepaint;

    public void AddInventory(IInventory inventory)
    {
        _inventories.Add(inventory);
    }

    private void MoveItem(int inventoryFrom, int indexFrom, int inventoryTo)
    {
        var fromI = _inventories[inventoryFrom];
        var toI = _inventories[inventoryTo];

        try
        {
            var item = fromI.PopItem(indexFrom);
            toI.PutItem(item);
        }
        catch
        {
            // ignored
        }
    }

    public async Task Start(CancellationToken token)
    {
        _needRepaint = true;
        await Task.WhenAll(Inputs(token), Draw(token));
    }

    private async Task Inputs(CancellationToken token)
    {
        using var inputReader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);

        while (!token.IsCancellationRequested)
        {
            try
            {
                if (!_needRepaint)
                {
                    _errors.Clear();
                    var line = await inputReader.ReadLineAsync(token);
                    if (!string.IsNullOrEmpty(line))
                    {
                        var strings = line.Split(' ');

                        if (strings.Length != 3)
                        {
                            _errors.Add("Не правильное количество аргументов");
                        }

                        var vals = strings.Select(s => (int.TryParse(s, out var val), val))
                            .Where(tuple => tuple.Item1)
                            .Select(tuple => tuple.val)
                            .ToArray();
                        if (vals.Length == 3)
                        {
                            MoveItem(vals[0], vals[1], vals[2]);
                        }
                        else
                        {
                            _errors.Add("Не правильные аргументы");
                        }
                    }
                    else
                    {
                        _errors.Add("Не правильное количество аргументов");
                    }

                    _needRepaint = true;
                }

                await Task.Yield();
            }
            catch
            {
                // ignored
            }
        }
    }

    private async Task Draw(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                if (_needRepaint)
                {
                    Console.Clear();

                    Console.Write(_currentView.Draw(_inventories));
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();

                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Join("\n", _errors.ToArray()));
                    Console.ForegroundColor = color;

                    Console.Write("Введите номер инвентаря, номер предмета и номер целевого инвентаря: ");
                    
                    _needRepaint = false;
                    await Task.Yield();
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}