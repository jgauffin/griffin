using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Converter
{
    /// <summary>
    /// Provides converters
    /// </summary>
    public interface IConverterProvider
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <returns></returns>
        IConverter<TFrom, TTo> Get<TFrom, TTo>();
    }
}
