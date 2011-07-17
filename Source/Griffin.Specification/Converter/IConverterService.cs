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
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Griffin.Converter
{
    /// <summary>
    /// Framework used to convert between different object types.
    /// </summary>
    /// <remarks>
    /// Converters are used to convert from one object to another. It can be used for simple types as from a <c>string</c> to a <c>DateTime</c>, but
    /// it can also be used to convert from an entity model to a view model. In other words, it can support any type of conversions
    /// as long as the correct providers are used. The purpose is to be able to create an abstration layer between different types of conversions
    /// and conversion frameworks (like automapper) and your code.
    /// </remarks>
    [ContractClass(typeof(IConverterServiceContract))]
    public interface IConverterService
    {
        /// <summary>
        /// Convert from a type to another.
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="from">Object to convert</param>
        /// <returns>Created object</returns>
        TTo Convert<TFrom, TTo>(TFrom from);

        /// <summary>
        /// Convert from a type to another.
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="from">Object to convert</param>
        /// <returns>Created object</returns>
        IEnumerable<TTo> ConvertAll<TFrom, TTo>(IEnumerable<TFrom> from);

        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="targetType">Type to convert to</param>
        /// <returns>Created object</returns>
        object Convert(object source, Type targetType);

        /// <summary>
        /// Register a new converter
        /// </summary>
        /// <typeparam name="TFrom">Type being converted</typeparam>
        /// <typeparam name="TTo">Type being created</typeparam>
        /// <param name="converter">Actual converter</param>
        /// <remarks>
        /// Any existing converter will be replaced with the new one.
        /// </remarks>
        void Register<TFrom, TTo>(IConverter<TFrom, TTo> converter);
    }
}