using System.Collections.Generic;
using System.Linq;

namespace Langy.Core.Extension;

public static class EnumerableExtension
{
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable)  where T: class
    {
        return enumerable
            .Where(i => i != null)
            .Cast<T>();
    }
}