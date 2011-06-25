using System;
using Griffin.Networking.Channels;

namespace Griffin.Networking.Messages
{
    public class CloseEvent : IChannelEvent
    {
        public IChannel Channel
        {
            get { throw new NotImplementedException(); }
        }
    }
}