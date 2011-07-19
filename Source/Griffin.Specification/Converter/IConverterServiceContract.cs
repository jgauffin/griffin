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
    [ContractClassFor(typeof(IConverterService))]
// ReSharper disable InconsistentNaming
    internal abstract class IConverterServiceContract : IConverterService
// ReSharper restore InconsistentNaming
    {
        public TTo Convert<TFrom, TTo>(TFrom from)
        {
            return default(TTo);
        }

        public IEnumerable<TTo> ConvertAll<TFrom, TTo>(IEnumerable<TFrom> from)
        {
            Contract.Requires<ArgumentNullException>(from != null);
            return null;
        }

        public object Convert(object source, Type targetType)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(targetType != null);
            return null;
        }

        public void Register<TFrom, TTo>(IConverter<TFrom, TTo> converter)
        {
            Contract.Requires<ArgumentNullException>(converter != null);
        }

        public void Register(IConverterProvider provider)
        {
            Contract.Requires<ArgumentNullException>(provider != null);
        }
    }
}
