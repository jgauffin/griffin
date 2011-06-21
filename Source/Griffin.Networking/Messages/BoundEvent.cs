using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Channels;

namespace Griffin.Core.Net.Messages
{
    public class BoundEvent : IChannelEvent
    {
        /// <summary>
        /// Gets channel that the event is for.
        /// </summary>
        public IChannel Channel { get; set; }
    }
}
