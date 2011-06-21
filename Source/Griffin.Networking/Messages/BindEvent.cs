using System.Net;
using Griffin.Core.Net.Channels;

namespace Griffin.Core.Net.Messages
{
    /// <summary>
    /// Bind a client or server to a specific end point.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Server channel: Used to bind the server to a local end point (that the server should listen to). You 
    /// can bind a server to several end points.
    /// </para>
    /// <para>
    /// Client channel: Specifies which remote end point that the channel should connect to.
    /// </para>
    /// </remarks>
    public class BindEvent : IChannelEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindEvent"/> class.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        public BindEvent(EndPoint endPoint, bool isSecure)
        {
            EndPoint = endPoint;
            IsSecure = isSecure;
        }

        /// <summary>
        /// Gets or sets the end point that should be used.
        /// </summary>
        public EndPoint EndPoint { get; private set; }

        /// <summary>
        /// Gets if only secure connections should be used
        /// </summary>
        public bool IsSecure { get; private set; }

        
    }
}