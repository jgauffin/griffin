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
    /// 
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
