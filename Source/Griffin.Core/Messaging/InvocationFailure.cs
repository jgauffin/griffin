using System;
using Griffin.Messaging;

namespace Griffin.Core.Messaging
{
    internal class InvocationFailure
    {
        public InvocationFailure(IMessage message, object handler, Exception err)
        {
            Message = message;
            Handler = handler;
            Exception = err;
        }

        public Exception Exception { get; set; }
        public object Handler { get; set; }
        public IMessage Message { get; set; }
    }
}