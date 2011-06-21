using Griffin.Core.Net.Messages;

namespace Griffin.Core.Net.Handlers
{
    public interface IContextCollection
    {
        /// <summary>
        /// Send a message down stream
        /// </summary>
        /// <param name="channelEvent">Event to send</param>
        void SendDownstream(IChannelEvent channelEvent);

        /// <summary>
        /// Send a message down stream (from application to channel)
        /// </summary>
        /// <param name="handler">Handler to start with</param>
        /// <param name="channelEvent">Event to send</param>
        /// <remarks>
        /// All handlers above the specified one will be ignored and not invoked.
        /// </remarks>
        void SendDownstream(IDownstreamHandler handler, IChannelEvent channelEvent);

        /// <summary>
        /// Send an event upstream (from channels to application)
        /// </summary>
        /// <param name="channelEvent">Event to send</param>
        void SendUpstream(IChannelEvent channelEvent);

        /// <summary>
        /// Send an event upstream (from channels to application)
        /// </summary>
        /// <param name="handlerToStartFrom">Handler that should be invoked</param>
        /// <param name="channelEvent">Event to send</param>
        void SendUpstream(IUpstreamHandler handlerToStartFrom, IChannelEvent channelEvent);

        /// <summary>
        /// Get context for a specific channel
        /// </summary>
        /// <param name="value"></param>
        IChannelHandlerContext Get(IChannelHandler value);
    }
}