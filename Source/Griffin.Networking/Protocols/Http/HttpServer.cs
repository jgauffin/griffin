using System;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;

namespace Griffin.Networking.Protocols.Http
{
    internal class HttpServer : IUpstreamHandler
    {
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
    }
}