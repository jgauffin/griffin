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
namespace Griffin.Messaging
{
    /// <summary>
    /// Context used when sending a Request/Response style message.
    /// </summary>
    public interface IRequestContext
    {

        /// <summary>
        /// Stop processing this request.
        /// </summary>
        /// <remarks>
        /// Invoked when the current handle have successfully
        /// handled the request, invoking more handlers
        /// would corrupt the response.
        /// </remarks>
        bool Cancel { get; set; }
    }

    /// <summary>
    /// Context used to invoke a request/reply message
    /// </summary>
    /// <typeparam name="TRequest">Type of request</typeparam>
    /// <typeparam name="TResponse">Type of response</typeparam>
    public interface IRequestContext<out TRequest, out TResponse> : IRequestContext
        where TRequest : IRequest 
        where TResponse : IResponse
    {
        /// <summary>
        /// Gets information about the request being processed.
        /// </summary>
        TRequest Request { get; }

        /// <summary>
        /// Gets object to add response information to.
        /// </summary>
        TResponse Response { get; }

    }
}
