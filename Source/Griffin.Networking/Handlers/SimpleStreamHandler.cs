using System;
using Griffin.Core;
using Griffin.Networking.Messages;

namespace Griffin.Networking.Handlers
{
    /// <summary>
    /// Convinience class making it easier to process events.
    /// </summary>
    public abstract class SimpleStreamHandler : IUpstreamHandler, IDownstreamHandler
    {
        #region IDownstreamHandler Members

        /// <summary>
        /// Handle the data that is going to be sent to the remote end point
        /// </summary>
        /// <param name="ctx">Context information</param>
        /// <param name="e">Chennel event.</param>
        public void HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUpstreamHandler Members

        void IUpstreamHandler.HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            if (e is ExceptionEvent)
                ExceptionCaught(ctx, e.As<ExceptionEvent>());
            else if (e is MessageEvent)
                HandleMessage(ctx, e.As<MessageEvent>());
            else if (e is BoundEvent)
                HandleBound(ctx, e.As<BoundEvent>());
            else if (e is ClosedEvent)
                HandleClosed(ctx, e.As<ClosedEvent>());
            else if (e is ConnectedEvent)
                HandleConnected(ctx, e.As<ConnectedEvent>());
        }

        #endregion

        /// <summary>
        /// An exception have been caught during processing.
        /// </summary>
        /// <param name="ctx">Channel context</param>
        /// <param name="e">Exception information</param>
        protected abstract void ExceptionCaught(IChannelHandlerContext ctx, ExceptionEvent e);

        /// <summary>
        /// A message have been received from the channel
        /// </summary>
        /// <param name="ctx">Channel context</param>
        /// <param name="e">Message information</param>
        protected abstract void HandleMessage(IChannelHandlerContext ctx, MessageEvent e);

        /// <summary>
        /// Channel have been connected.
        /// </summary>
        /// <param name="ctx">Context unique for this handler/channel combination.</param>
        /// <param name="e">Event information</param>
        protected abstract void HandleConnected(IChannelHandlerContext ctx, ConnectedEvent e);

        /// <summary>
        /// Channel have been closed.
        /// </summary>
        /// <param name="ctx">Context unique for this handler/channel combination.</param>
        /// <param name="e">Event information</param>
        protected abstract void HandleClosed(IChannelHandlerContext ctx, ClosedEvent e);

        /// <summary>
        /// Channel have been bound to a specific address.
        /// </summary>
        /// <param name="ctx">Context unique for this handler/channel combination.</param>
        /// <param name="e">Event information</param>
        protected abstract void HandleBound(IChannelHandlerContext ctx, BoundEvent e);
    }
}