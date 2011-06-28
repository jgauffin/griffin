using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Converter;

namespace Griffin.Core.Converters
{
    /// <summary>
    /// Convert from string to DateTime using the default parse method.
    /// </summary>
    public class StringToDateTimeConverter : IConverter<string, DateTime>
    {
        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        public DateTime Convert(string source)
        {
            return DateTime.Parse(source);
        }
    }
}
