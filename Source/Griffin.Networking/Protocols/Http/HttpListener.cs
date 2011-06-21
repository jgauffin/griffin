using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Messages;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.Protocols.Http
{
    class HttpListener : IUpstreamHandler, IPipelineFactory
    {
        List<TcpServerChannel> _channels = new List<TcpServerChannel>();

        public void AddEndPoint(IPEndPoint listenerAddress, bool isSecure)
        {
            
        }

        /// <summary>
        /// Process data that was received from the channel.
        /// </summary>
        /// <param name="ctx">Context which is used for the currently processed channel</param>
        /// <param name="e">Event being processed.</param>
        public void HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new pipeline with all attached channel handlers.
        /// </summary>
        public IPipeline CreatePipeline()
        {
            var pipeline = new Pipeline();
            pipeline.RegisterDownstreamHandler(new Encoder());
            pipeline.RegisterUpstreamHandler(new Decoder());
            return pipeline;
        }
    }
}
