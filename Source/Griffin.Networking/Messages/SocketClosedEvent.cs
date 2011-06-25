using System.Net;
using System.Net.Sockets;

namespace Griffin.Networking.Messages
{
    public class SocketClosedEvent : ClosedEvent
    {
        public SocketClosedEvent(EndPoint localEndPoint, EndPoint remoteEndPoint, SocketError error)
            : base(localEndPoint, remoteEndPoint)
        {
            ErrorCode = error;
        }

        /// <summary>
        /// Get socket error code
        /// </summary>
        public SocketError ErrorCode { get; set; }
    }
}