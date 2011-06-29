using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Messaging
{
    /// <summary>
    /// Context used to invoke a request/reply message
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IRequestContext<TRequest, TResponse> : IRequestContext
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
}
