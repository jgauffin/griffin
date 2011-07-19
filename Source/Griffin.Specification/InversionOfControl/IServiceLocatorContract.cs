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
using System.Linq;
using System.Text;

namespace Griffin.InversionOfControl
{
#pragma warning disable 1591
// ReSharper disable InconsistentNaming
    [ContractClassFor(typeof(IServiceLocator))]
    internal abstract class IServiceLocatorContract : IServiceLocator
// ReSharper restore InconsistentNaming
#pragma warning restore 1591
    {
        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
            return null;
        }

        public T Resolve<T>() where T : class
        {
            return null;
        }

        public object Resolve(Type serviceType)
        {
            Contract.Requires<ArgumentNullException>(serviceType != null);
            return null;
        }
    }
}
