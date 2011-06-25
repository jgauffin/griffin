using Griffin.Networking.Messages;

namespace Griffin.Networking.Handlers
{
    /// <summary>
    /// Context sensitive information for each event in a channel.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Context is used to let a channel handler keep it's state for a specific channel. The context is therefore
    /// unique for a specific handler in a specific channel.
    /// </para>
    /// </remarks>
    public interface IChannelHandlerContext
    {
        /// <summary>
        /// Gets or sets a state for this specific combination of channel and handler.
        /// </summary>
        object State { get; set; }

        /// <summary>
        /// Gets name of this context.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Continue to move the event towards the channel
        /// </summary>
        /// <param name="channelEvent">Event being processed.</param>
        void SendDownstream(IChannelEvent channelEvent);

        /// <summary>
        /// Continue to move the event towards your application.
        /// </summary>
        /// <param name="channelEvent">Event being processed.</param>
        void SendUpstream(IChannelEvent channelEvent);
    }
}