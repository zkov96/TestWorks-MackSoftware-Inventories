namespace Inventories.Interfaces;

public interface IItem
{
    /// <summary>
    /// Название предмета
    /// </summary>
    string Name { get; }

    int GetMinStringSize()
    {
        return Name.Length;
    }

    string GetNormalizedString(int size)
    {
        return Name;
    }
}