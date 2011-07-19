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
    public interface IHandlerOf<TRequestContext>
        where TRequestContext : IRequestContext 
    {
        void ProcessRequest(TRequestContext context);
    }


    public class FindContactsRequest : IRequest
    {
        public string SearchValue { get; set; }
    }

    public class FindContactsResponse : IResponse
    {
        public ICollection<string> Contacts { get; set; }
    }

    public class FindContactsContext : IRequestContext<FindContactsRequest, FindContactsResponse>
    {
        /// <summary>
        /// Stop processing this request.
        /// </summary>
        /// <remarks>
        /// Invoked when the current handle have successfully
        /// handled the request, invoking more handlers
        /// would corrupt the response.
        /// </remarks>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets information about the request being processed.
        /// </summary>
        public FindContactsRequest Request { get; private set; }

        /// <summary>
        /// Gets object to add response information to.
        /// </summary>
        public FindContactsResponse Response { get; private set; }
    }

    public class UserService : IHandlerOf<FindContactsContext>
    {
        public void ProcessRequest(FindContactsContext context)
        {
            if (context.Request.SearchValue == "blabla")
            {
                context.Response.Contacts.Add("My contact name");
            }
        }
    }
}
