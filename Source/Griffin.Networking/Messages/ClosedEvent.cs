using System;
using System.Net;
using Griffin.Core.Net.Channels;

namespace Griffin.Core.Net.Messages
{
    public class ClosedEvent : IUpstreamEvent
    {
        public ClosedEvent(EndPoint localEndPoint, EndPoint remoteEndPoint)
        {
            RemoteEndPoint = remoteEndPoint;
            LocalEndPoint = localEndPoint;
        }

        /// <summary>
        /// Gets or sets local end point (that you are bound to)
        /// </summary>
        public EndPoint LocalEndPoint { get; set; }

        /// <summary>
        /// Gets or sets remote end point
        /// </summary>
        /// <remarks>
        /// If you are a server, this is the client that disconnected. If you are a client, this is the IP that you got disconnected from
        /// </remarks>
        public EndPoint RemoteEndPoint { get; set; }

        #region IUpstreamEvent Members

        public IChannel Channel
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}