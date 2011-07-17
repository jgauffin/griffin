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

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// A class that should be registered as a service in an inversion of control container.
    /// </summary>
    /// <remarks>
    /// The purpose of the attribute is to be able to load service implementations into an inversion of control
    /// container. All interfaces (except .NET framework interfaces) should be registered for the class that has
    /// this attribute.
    /// </remarks>
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentAttribute"/> class.
        /// </summary>
        public ComponentAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentAttribute"/> class.
        /// </summary>
        /// <param name="flags">Specifies how the implementation should be created/maintained.</param>
        /// <example>
        /// <code>
        /// [Component(ComponentFlags.Singleton)]
        /// public class CachedUserService : IUserService
        /// {
        /// }
        /// </code>
        /// </example>
        public ComponentAttribute(ComponentFlags flags)
        {
            Flags = flags;
        }

        /// <summary>
        /// Gets flags defining how to instantiate the object etc.
        /// </summary>
        public ComponentFlags Flags { get; private set; }
    }
}