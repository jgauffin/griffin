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

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// Used to locate registered services.
    /// </summary>
    /// <seealso cref="IContainerBuilder"/>.
    [ContractClass(typeof(IServiceLocatorContract))]
    public interface IServiceLocator
    {
        /// <summary>
        /// Find all registered instances of a type
        /// </summary>
        /// <typeparam name="T">Type of service to locate</typeparam>
        /// <returns>All found implemententations or an empty collection.</returns>
        /// <remarks>
        /// Some inversion of control containers (such as Unity) do not allow 
        /// </remarks>
        IEnumerable<T> ResolveAll<T>() where T : class;

        /// <summary>
        /// Locate a service.
        /// </summary>
        /// <typeparam name="T">Type of service to locate</typeparam>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// Locate a service.
        /// </summary>
        /// <param name="serviceType">Type of service to locate</param>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        object Resolve(Type serviceType);
    }
}