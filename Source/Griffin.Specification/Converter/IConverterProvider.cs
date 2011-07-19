﻿/*
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
using System.Linq;
using System.Text;

namespace Griffin.Converter
{
    /// <summary>
    /// A provider is used to provide a certain kind of conversion to the layer. 
    /// </summary>
    /// <remarks>
    /// </remarks>
    public interface IConverterProvider
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <returns>Converter if found; otherwise <c>null</c>.</returns>
        /// <remarks>Simply return a converter if the the provider supports it. The converter service
        /// will cache your converter and it will not be asked for again during the applications lifetime.</remarks>
        IConverter<TFrom, TTo> Get<TFrom, TTo>();
    }
}
