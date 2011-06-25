using System.Net.Sockets;
using Griffin.Networking.Buffers;

namespace Griffin.Networking.Channels
{
    public class TcpChannelConfig
    {
        public TcpChannelConfig(BufferManager bufferManager, IObjectPool<SocketAsyncEventArgs> objectPool)
        {
            BufferManager = bufferManager;
            SocketAsyncEventPool = objectPool;
        }

        public BufferManager BufferManager { get; private set; }

        public Socket Socket { get; set; }
        public IObjectPool<SocketAsyncEventArgs> SocketAsyncEventPool { get; private set; }
    }
}