namespace Inventories.Utils;

public static class StringUtils
{
    public static string NormalizeSize(this string str, int size)
    {
        return string.Format($"{{0, {size}}}", str);
    }
}