namespace Inventories.Interfaces;

public interface IInventory
{
    /// <summary>
    /// Название инвентаря
    /// </summary>
    string Name { get; }
    
    int ItemsCount { get; }
    
    /// <summary>
    /// Отдаёт список предметов
    /// </summary>
    /// <returns></returns>
    IEnumerable<IItem> GetItems();
    
    /// <summary>
    /// Кладёт предмет в инвентарь
    /// </summary>
    /// <param name="item"></param>
    int PutItem(IItem item);
    
    /// <summary>
    /// Забирает предмет из инвентаря
    /// </summary>
    /// <param name="index"></param>
    IItem PopItem(int index);

}