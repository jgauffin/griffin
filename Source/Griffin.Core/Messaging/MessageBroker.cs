using System;
using System.Collections.Generic;
using System.Threading;
using Griffin.InversionOfControl;
using Griffin.Logging;
using Griffin.Messaging;

namespace Griffin.Core.Messaging
{
    /// <summary>
    /// Keeps track of all subscribers and provides an inteface for the publishers.
    /// </summary>
    /// <remarks>
    /// Uses an inversion of control container to lookup the subscribers.
    /// </remarks>
    public class MessageBroker : IMessageBroker
    {
        private readonly ILogger _logger = LogManager.GetLogger<IMessageBroker>();
        private readonly IServiceLocator _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBroker"/> class.
        /// </summary>
        /// <param name="resolver">Inversion of control container used to find subscribers.</param>
        public MessageBroker(IServiceLocator resolver)
        {
            _resolver = resolver;
        }

        /// <summary>
        /// Invoke all subsribers for an message
        /// </summary>
        /// <param name="state">The state.</param>
        /// <remarks>
        /// Called from the thread pool to keep everything synchronous. Hence it's vital
        /// that this method do not throw any exceptions.
        /// </remarks>
        private void InvokeMessage(object state)
        {
            var job = (IInvoker) state;
            IEnumerable<InvocationFailure> errors = job.Invoke();
            foreach (InvocationFailure invocationFailure in errors)
            {
                _logger.Warning(
                    "'" + invocationFailure.Handler + "' failed to handle '" + invocationFailure.Message + "'",
                    invocationFailure.Exception);
            }
        }


        /// <summary>
        /// Invokes the request/reply instance. 
        /// </summary>
        /// <param name="state">The state.</param>
        private void InvokeRequest(object state)
        {
            var asyncResult = (RequestAsyncResultBase) state;
            asyncResult.Invoke();
        }

        #region IMessageBroker Members

        public void Publish<T>(T message) where T : IMessage
        {
            IEnumerable<ISubscriberOf<T>> subscribers = _resolver.ResolveAll<ISubscriberOf<T>>();
            var invoker = new Invoker<T>(subscribers, message);

            ThreadPool.QueueUserWorkItem(InvokeMessage, invoker);
        }

        public IAsyncResult BeginRequest<TContext>(TContext context, AsyncCallback callback, object state)
            where TContext : IRequestContext
        {
            IEnumerable<IHandlerOf<TContext>> subscribers = _resolver.ResolveAll<IHandlerOf<TContext>>();
            var asyncResult = new RequestAsyncResult<TContext>
                                  {
                                      Subscribers = subscribers,
                                      Context = context,
                                      AsyncState = state
                                  };
            ThreadPool.QueueUserWorkItem(InvokeRequest, asyncResult);
            return asyncResult;
        }

        public void EndRequest(IAsyncResult result)
        {
            result.AsyncWaitHandle.WaitOne();
        }

        #endregion
    }
}