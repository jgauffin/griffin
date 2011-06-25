using System;
using Griffin.Networking.Channels;

namespace Griffin.Networking.Messages
{
    public class MessageEvent : IChannelEvent
    {
        public MessageEvent(object message)
        {
            Message = message;
        }

        public object Message { get; set; }

        public IChannel Channel
        {
            get { throw new NotImplementedException(); }
        }
    }
}