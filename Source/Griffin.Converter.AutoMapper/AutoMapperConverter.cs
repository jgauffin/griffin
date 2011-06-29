/*
 * Copyright (c) 2011, Jonas Gauffin. All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301 USA
 */
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
