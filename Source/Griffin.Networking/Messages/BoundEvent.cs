using Griffin.Networking.Channels;

namespace Griffin.Networking.Messages
{
    public class BoundEvent : IChannelEvent
    {
        /// <summary>
        /// Gets channel that the event is for.
        /// </summary>
        public IChannel Channel { get; set; }
    }
}