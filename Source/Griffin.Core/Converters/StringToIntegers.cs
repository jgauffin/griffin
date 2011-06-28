using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Converter;

namespace Griffin.Core.Converters
{
    /// <summary>
    /// Converters for <c>int</c>, <c>long</c> and <c>short</c>.
    /// </summary>
    public class StringToIntegers : IConverter<string, int>, IConverter<string, short>, IConverter<string, long>
    {
        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        public int Convert(string source)
        {
            return int.Parse(string.IsNullOrEmpty(source) ? "0" : source);
        }

        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        short IConverter<string, short>.Convert(string source)
        {
            return short.Parse(string.IsNullOrEmpty(source) ? "0" : source);
        }

        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        long IConverter<string, long>.Convert(string source)
        {
            return long.Parse(string.IsNullOrEmpty(source) ? "0" : source);
        }
    }
}
