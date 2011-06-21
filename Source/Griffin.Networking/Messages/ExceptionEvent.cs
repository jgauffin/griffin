using System;
using Griffin.Core.Net.Channels;

namespace Griffin.Core.Net.Messages
{
    public class ExceptionEvent : IChannelEvent
    {
        public Exception Exception { get; set; }

        public ExceptionEvent(Exception exception)
        {
            Exception = exception;
        }

        #region IChannelEvent Members

        public IChannel Channel
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}