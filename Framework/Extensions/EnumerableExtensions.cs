namespace Framework.Extensions;

public static class EnumerableExtensions
{
    public static bool AddIfNotContain<T>(this IList<T> list, T item) {
        if (list.Contains(item)) {
            return false;
        }

        list.Add(item);
        return true;
    }
}
