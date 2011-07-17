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
namespace Griffin.Converter
{
    /// <summary>
    /// Use to convert from a specific type to another.
    /// </summary>
    /// <remarks>
    /// Converters are used to convert from one object to another. It can be used for simple types as from a <c>string</c> to a <c>DateTime</c>, but
    /// it can also be used to convert from an entity model to a view model. In other words, it can support any type of conversions
    /// as long as the correct providers are used. The purpose is to be able to create an abstration layer between different types of conversions
    /// and conversion frameworks (like automapper) and your code.
    /// </remarks>
    public interface IConverter<in TFrom, out TTo>
    {
        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        TTo Convert(TFrom source);
    }
}
