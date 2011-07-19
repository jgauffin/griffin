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
using Griffin.Messaging;

namespace Griffin.Messaging
{
    /// <summary>
    /// Used to handle a request in the event system
    /// </summary>
    /// <typeparam name="TRequestContext">Message context</typeparam>
    public interface IHandlerOf<in TRequestContext>
        where TRequestContext : IRequestContext 
    {
        /// <summary>
        /// Process the requested message
        /// </summary>
        /// <param name="context">The context containing request/response.</param>
        void ProcessRequest(TRequestContext context);
    }


}
