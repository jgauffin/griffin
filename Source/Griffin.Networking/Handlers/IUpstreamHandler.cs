using Griffin.Core.Net.Messages;

namespace Griffin.Core.Net.Handlers
{
    /// <summary>
    /// Processes data that is moving from the channel towards the application.
    /// </summary>
    public interface IUpstreamHandler : IChannelHandler
    {
        /// <summary>
        /// Process data that was received from the channel.
        /// </summary>
        /// <param name="ctx">Context which is used for the currently processed channel</param>
        /// <param name="e">Event being processed.</param>
        void HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e);
    }
}