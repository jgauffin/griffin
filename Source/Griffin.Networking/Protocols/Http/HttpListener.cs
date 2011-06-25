using System;
using System.Collections.Generic;
using System.Net;
using Griffin.Networking.Channels;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;
using Griffin.Networking.Pipelines;

namespace Griffin.Networking.Protocols.Http
{
    internal class HttpListener : IUpstreamHandler, IPipelineFactory
    {
        private List<TcpServerChannel> _channels = new List<TcpServerChannel>();
        private IPipeline _clientPipeline;
        private IPipeline _serverPipeline;

        #region IPipelineFactory Members

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

        #endregion

        #region IUpstreamHandler Members

        /// <summary>
        /// Process data that was received from the channel.
        /// </summary>
        /// <param name="ctx">Context which is used for the currently processed channel</param>
        /// <param name="e">Event being processed.</param>
        public void HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void AddEndPoint(IPEndPoint listenerAddress, bool isSecure)
        {
            var channel = new TcpServerChannel(_serverPipeline, _clientPipeline, 5000);
        }
    }
}