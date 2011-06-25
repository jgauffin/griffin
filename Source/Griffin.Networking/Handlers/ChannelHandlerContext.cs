using System;
using Griffin.Networking.Channels;
using Griffin.Networking.Messages;

namespace Griffin.Networking.Handlers
{
    /// <summary>
    /// Context for a specific handler in a specific channel.
    /// </summary>
    /// <remarks>
    /// Contexts are used to keep track of specific handler in a specific channel. 
    /// </remarks>
    public class ChannelHandlerContext : IChannelHandlerContext
    {
        private readonly IChannel _channel;
        private readonly IContextCollection _contextCollection;
        private IDownstreamHandler _nextDownstream;
        private IUpstreamHandler _nextUpstream;

        public ChannelHandlerContext(IChannel channel, IContextCollection contextCollection)
        {
            _channel = channel;
            _contextCollection = contextCollection;
        }

        internal IDownstreamHandler NextDownstreamHandler
        {
            set { _nextDownstream = value; }
        }

        internal IUpstreamHandler NextUpstreamHandler
        {
            set { _nextUpstream = value; }
        }

        #region IChannelHandlerContext Members

        /// <summary>
        /// Continue to move the event towards your application.
        /// </summary>
        /// <param name="channelEvent">Event being processed.</param>
        public void SendUpstream(IChannelEvent channelEvent)
        {
            if (_nextUpstream != null)
                _nextUpstream.HandleUpstream(_contextCollection.Get(_nextUpstream), channelEvent);
            else
                throw new InvalidOperationException("There are no more upstream handlers.");
        }

        /// <summary>
        /// Gets name of this context.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Continue to move the event towards the channel
        /// </summary>
        /// <param name="channelEvent">Event being processed.</param>
        public void SendDownstream(IChannelEvent channelEvent)
        {
            if (_nextDownstream != null)
                _nextDownstream.HandleDownstream(_contextCollection.Get(_nextDownstream), channelEvent);
            else
                throw new InvalidOperationException("There are no more downstream handlers.");
        }

        /// <summary>
        /// Gets or sets a state for this specific combination of channel and handler.
        /// </summary>
        public object State { get; set; }

        #endregion
    }
}