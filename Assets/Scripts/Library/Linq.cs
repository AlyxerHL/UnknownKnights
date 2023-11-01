using System;
using System.Collections.Generic;

public static class Linq
{
    public static TSource MinBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector
    )
        where TKey : IComparable<TKey>
    {
        TSource minSource = default;
        TKey minKey = default;

        foreach (var item in source)
        {
            var value = keySelector(item);

            if (minSource == null || value.CompareTo(minKey) < 0)
            {
                minSource = item;
                minKey = value;
            }
        }

        return minSource;
    }

    public static TSource MaxBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector
    )
        where TKey : IComparable<TKey>
    {
        TSource maxSource = default;
        TKey maxKey = default;

        foreach (var item in source)
        {
            var value = keySelector(item);

            if (maxSource == null || value.CompareTo(maxKey) > 0)
            {
                maxSource = item;
                maxKey = value;
            }
        }

        return maxSource;
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action.Invoke(item);
        }
    }
}
