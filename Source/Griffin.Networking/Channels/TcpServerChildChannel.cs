using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.Channels
{
    /// <summary>
    /// Channel for an accepted endpoint in a server.
    /// </summary>
    class TcpServerChildChannel : TcpChannel
    {
        public TcpServerChildChannel(IPipeline pipeline) : base(pipeline)
        {
        }
    }
}
