using System;
using Griffin.Core.Net.Channels;

namespace Griffin.Core.Net.Messages
{
    public class CloseEvent : IChannelEvent
    {
        #region IChannelEvent Members

        public IChannel Channel
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}