using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComicsStore.MiddleWare.Common
{
    public class CollectionHelper<T>
    {
        public static bool IsEqual(IEnumerable<T> A, IEnumerable<T> B, IEqualityComparer<T> comparer)
        {
            return (A.Count() == B.Count() && (!A.Except(B, comparer).Any() || !B.Except(A, comparer).Any()));
        }

        public static IEnumerable<T> Except(IEnumerable<T> A, IEnumerable<T> B, IEqualityComparer<T> comparer)
        {
            return A.Except(B, comparer);
        }


        /* Move to Extensions
        var c = a.ExceptBy(b, x => x.Id);

var r = p.ExceptBy(q, x => x.Name, StringComparer.OrdinalIgnoreCase);

// ...

public static class EnumerableExtensions
{
    public static IEnumerable<TSource> ExceptBy<TSource, TKey>(
        this IEnumerable<TSource> first, IEnumerable<TSource> second,
        Func<TSource, TKey> keySelector,
        IEqualityComparer<TKey> keyComparer = null)
    {
        if (first == null) throw new ArgumentNullException("first");
        if (second == null) throw new ArgumentNullException("second");
        if (keySelector == null) throw new ArgumentNullException("keySelector");

        return first.ExceptByIterator(second, keySelector, keyComparer);
    }

    private static IEnumerable<TSource> ExceptByIterator<TSource, TKey>(
        this IEnumerable<TSource> first, IEnumerable<TSource> second,
        Func<TSource, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
    {
        var keys = new HashSet<TKey>(second.Select(keySelector), keyComparer);

        foreach (TSource item in first)
        {
            if (keys.Add(keySelector(item)))
                yield return item;
        }
    }
}
         */
    }
}
