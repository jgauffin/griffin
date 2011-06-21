using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Messages;

namespace Griffin.Core.Net.Protocols.Http
{
    /// <summary>
    /// Represents a specific channel
    /// </summary>
    class HttpChildChannel : SimpleStreamHandler
    {
        /// <summary>
        /// Channel have been closed.
        /// </summary>
        public event EventHandler Closed = delegate { };

        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException = delegate { };

        /// <summary>
        /// An exception have been caught during processing.
        /// </summary>
        /// <param name="ctx">Channel context</param>
        /// <param name="e">Exception information</param>
        protected override void ExceptionCaught(IChannelHandlerContext ctx, ExceptionEvent e)
        {
            UnhandledException(this, new UnhandledExceptionEventArgs("An exception was caught in a channel", e.Exception));
        }

        /// <summary>
        /// A message have been received from the channel
        /// </summary>
        /// <param name="ctx">Channel context</param>
        /// <param name="e">Message information</param>
        protected override void HandleMessage(IChannelHandlerContext ctx, MessageEvent e)
        {
            
        }

        /// <summary>
        /// Channel have been connected.
        /// </summary>
        /// <param name="ctx">Context unique for this handler/channel combination.</param>
        /// <param name="e">Event information</param>
        protected override void HandleConnected(IChannelHandlerContext ctx, ConnectedEvent e)
        {
            
        }

        /// <summary>
        /// Channel have been closed.
        /// </summary>
        /// <param name="ctx">Context unique for this handler/channel combination.</param>
        /// <param name="e">Event information</param>
        protected override void HandleClosed(IChannelHandlerContext ctx, ClosedEvent e)
        {
            
        }

        /// <summary>
        /// Channel have been bound to a specific address.
        /// </summary>
        /// <param name="ctx">Context unique for this handler/channel combination.</param>
        /// <param name="e">Event information</param>
        protected override void HandleBound(IChannelHandlerContext ctx, BoundEvent e)
        {
            
        }
    }
}
