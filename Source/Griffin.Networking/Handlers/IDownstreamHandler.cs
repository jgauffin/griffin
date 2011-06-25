using Griffin.Networking.Messages;

namespace Griffin.Networking.Handlers
{
    /// <summary>
    /// Used to modify content before it reaches the channel
    /// </summary>
    /// <remarks>
    /// In a downstream, everything is sent in the pipeline from your
    /// application down to the channel.
    /// </remarks>
    public interface IDownstreamHandler : IChannelHandler
    {
        /// <summary>
        /// Handle the data that is going to be sent to the remote end point
        /// </summary>
        /// <param name="ctx">Context information</param>
        /// <param name="e">Chennel event.</param>
        void HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e);
    }
}