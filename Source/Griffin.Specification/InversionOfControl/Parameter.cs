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
using System.Diagnostics.Contracts;

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// Gets a parameter used when configuring the inversion of control container.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="name">Constructor argument name.</param>
        /// <param name="value">The value.</param>
        public Parameter(string name, object value)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(name));
            Contract.Requires<ArgumentNullException>(value != null);

            Name = name;
            Value = value;
        }

        /// <summary>
        /// Get parameter name
        /// </summary>
        /// <remarks>
        /// It should correspond to the name of the constructor argument.
        /// </remarks>
        public string Name { get; private set; }

        /// <summary>
        /// Gets value to use
        /// </summary>
        public object Value { get; private set; }
    }
}