namespace Inventories.Interfaces;

public interface IView
{
    string Draw(IEnumerable<IInventory> inventories);
}