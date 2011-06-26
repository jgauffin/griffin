using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Griffin.Core
{
    /// <summary>
    /// IENumerable extensions
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Invoke an action for each element
        /// </summary>
        /// <typeparam name="T">Type being visited</typeparam>
        /// <param name="list">List to traverse</param>
        /// <param name="action">Action to invoke</param>
        public static void Each<T>(this IEnumerable<T> list, Action<T> action)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            if (list == null)
                return;

            foreach (T item in list)
            {
                action(item);
            }
        }
    }
}