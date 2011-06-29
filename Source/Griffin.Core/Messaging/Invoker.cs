using System;
using System.Collections.Generic;
using Griffin.Messaging;

namespace Griffin.Core.Messaging
{
    internal class Invoker<T> : IInvoker where T : IMessage
    {
        private readonly T _message;
        private readonly IEnumerable<ISubscriberOf<T>> _subscribers;

        public Invoker(IEnumerable<ISubscriberOf<T>> subscribers, T message)
        {
            _subscribers = subscribers;
            _message = message;
        }

        #region IInvoker Members

        public IEnumerable<InvocationFailure> Invoke()
        {
            var errors = new LinkedList<InvocationFailure>();
            foreach (var subscriber in _subscribers)
            {
                try
                {
                    subscriber.ProcessMessage(_message);
                }
                catch (Exception err)
                {
                    errors.AddLast(new InvocationFailure(_message, subscriber, err));
                }
            }

            return errors;
        }

        #endregion
    }
}