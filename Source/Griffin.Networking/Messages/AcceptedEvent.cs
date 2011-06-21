using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Channels;

namespace Griffin.Core.Net.Messages
{
    class AcceptedEvent : IServerEvent
    {
        public IChannel ClientChannel { get; set; }
    }
}
