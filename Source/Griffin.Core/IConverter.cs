using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core
{
    /// <summary>
    /// Use to convert from a specific type to another.
    /// </summary>
    /// <remarks>
    /// The conversion is not a regular casting but an attempt to make a real conversion.
    /// </remarks>
    public interface IConverter<TFrom, TTo>
    {
        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        TTo Convert(TFrom source);
    }
}
