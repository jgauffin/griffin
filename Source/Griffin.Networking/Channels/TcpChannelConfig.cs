using System.Net.Sockets;
using Griffin.Core.Net.Buffers;

namespace Griffin.Core.Net.Channels
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