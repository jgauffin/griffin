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
using System.Linq;
using System.Text;

namespace Griffin.Messaging
{
    /// <summary>
    /// Broker used to publish messages in the system
    /// </summary>
    /// <remarks>
    /// The heart of the publish/subscribe messaging framework. The goal with the fr
    /// </remarks>
    public interface IMessageBroker
    {
        /// <summary>
        /// Start a request/reply action
        /// </summary>
        /// <typeparam name="TContext">Type of request/reply context.</typeparam>
        /// <param name="context">Context containing the requst and reply objects.</param>
        /// <param name="callback">Callback to ivoke when the request have been processed by all handlers</param>
        /// <param name="state">State being passed to the callback. Use it to store any context information.</param>
        /// <returns>AsyncResult used to keep track of the request.</returns>
        IAsyncResult BeginRequest<TContext>(TContext context, AsyncCallback callback, object state)
            where TContext : IRequestContext;

        /// <summary>
        /// Wait for the request to complete
        /// </summary>
        /// <param name="result">Result returned by <see cref="BeginRequest{T}"/>.</param>
        void EndRequest(IAsyncResult result);

        /// <summary>
        /// Publish a message in the system
        /// </summary>
        /// <typeparam name="T">Type of message</typeparam>
        /// <param name="message">Actual message</param>
        /// <remarks>
        /// Is being sent asynchronously.
        /// </remarks>
        void Publish<T>(T message) where T : IMessage;
    }
}
