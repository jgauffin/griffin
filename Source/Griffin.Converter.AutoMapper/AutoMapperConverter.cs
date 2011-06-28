using System.Collections.Generic;
using AutoMapper;

namespace Griffin.Converter.AutoMapper
{
    /// <summary>
    /// Adds support for conversion through auto mapper.
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    public class AutoMapperConverter<TFrom, TTo> : IConverter<TFrom, TTo>, IConverter<IEnumerable<TFrom>, IEnumerable<TTo>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperConverter&lt;TFrom, TTo&gt;"/> class.
        /// </summary>
        public AutoMapperConverter()
        {
            Mapper.CreateMap(typeof (TFrom), typeof (TTo));
            Mapper.CreateMap(typeof (IEnumerable<TFrom>), typeof (IEnumerable<TTo>));
        }

        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        public TTo Convert(TFrom source)
        {
            return Mapper.Map<TFrom, TTo>(source);
        }

        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        public IEnumerable<TTo> Convert(IEnumerable<TFrom> source)
        {
            return Mapper.Map<IEnumerable<TFrom>, IEnumerable<TTo>>(source);
        }
    }
}
