using System.Net;
using System.Net.Sockets;

namespace Griffin.Core.Net.Messages
{
    public class SocketClosedEvent : ClosedEvent
    {
        /// <summary>
        /// Get socket error code
        /// </summary>
        public SocketError ErrorCode { get; set; }

        public SocketClosedEvent(EndPoint localEndPoint,EndPoint remoteEndPoint, SocketError error) : base(localEndPoint, remoteEndPoint)
        {
            ErrorCode = error;
        }
    }
}