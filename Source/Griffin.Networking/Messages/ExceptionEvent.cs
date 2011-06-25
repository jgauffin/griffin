using System;
using Griffin.Networking.Channels;

namespace Griffin.Networking.Messages
{
    public class ExceptionEvent : IChannelEvent
    {
        public ExceptionEvent(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; set; }

        public IChannel Channel
        {
            get { throw new NotImplementedException(); }
        }
    }
}