﻿using Griffin.Networking.Channels;

namespace Griffin.Networking.Messages
{
    /// <summary>
    /// Connect to a remote end point
    /// </summary>
    /// <remarks>
    /// Used for client channels when connecting to remote end points. You must have used a <see cref="BindEvent"/> before
    /// sending a connect event.
    /// </remarks>
    internal class ConnectEvent : IChannelEvent
    {
        /// <summary>
        /// Gets channel that the event is for.
        /// </summary>
        public IChannel Channel { get; set; }
    }
}