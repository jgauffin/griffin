using System.Net;
using Griffin.Networking.Channels;

namespace Griffin.Networking.Messages
{
    public class ConnectedEvent : IUpstreamEvent
    {
        public ConnectedEvent(EndPoint localEndPoint, EndPoint remoteEndPoint)
        {
            LocalEndPoint = localEndPoint;
            RemoteEndPoint = remoteEndPoint;
        }

        /// <summary>
        /// Gets or sets local end point
        /// </summary>
        public EndPoint LocalEndPoint { get; set; }

        /// <summary>
        /// Gets or sets remote end point
        /// </summary>
        public EndPoint RemoteEndPoint { get; set; }

        public IChannel Channel
        {
            get { return null; }
        }
    }
}