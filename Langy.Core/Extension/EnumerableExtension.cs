using System;
using System.Collections.Generic;
using System.Linq;

namespace Langy.Core.Extension;

public static class EnumerableExtension
{
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable)  where T : class
    {
        return enumerable
            .Where(i => i != null)
            .Cast<T>();
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var item in enumerable)
        {
            action(item);
        }
    }
    
    public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        items.ForEach(collection.Add);
        return collection;
    }
}