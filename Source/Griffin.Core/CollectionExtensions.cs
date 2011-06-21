using System;
using System.Collections.Generic;

namespace Griffin.Core
{
    public static class CollectionExtensions
    {
        public static void Each<T>(this IEnumerable<T> list, Action<T> action)
    {
            foreach (var item in list)
            {
                action(item);
            }
    }
    }
}
