using System;
using Griffin.Core.Net.Channels;

namespace Griffin.Core.Net.Messages
{
    public class MessageEvent : IChannelEvent
    {
        public MessageEvent(object message)
        {
            Message = message;
        }

        public object Message { get; set; }

        #region IChannelEvent Members

        public IChannel Channel
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}