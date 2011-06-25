using System.Net.Sockets;
using Griffin.Networking.Buffers;
using Griffin.Networking.Pipelines;

namespace Griffin.Networking.Channels
{
    internal class TcpClientChannelFactory : IChannelFactory
    {
        private readonly ObjectPool<SocketAsyncEventArgs> _asyncArgsFactory;
        private readonly BufferManager _bufferManager;

        public TcpClientChannelFactory()
        {
            _bufferManager = new BufferManager(10, 65535);
            _bufferManager.CreateBuffers();
            _asyncArgsFactory = new ObjectPool<SocketAsyncEventArgs>(CreateAsyncArgs);
        }

        #region IChannelFactory Members

        public IChannel CreateChannel(IPipeline pipeline)
        {
            var channel = new TcpClientChannel(pipeline);
            channel.Initialize(CreateConfig());
            return channel;
        }

        #endregion

        private SocketAsyncEventArgs CreateAsyncArgs()
        {
            var args = new SocketAsyncEventArgs();
            return args;
        }

        protected virtual TcpChannelConfig CreateConfig()
        {
            return new TcpChannelConfig(_bufferManager, _asyncArgsFactory)
                       {
                           Socket = CreateSocket(),
                       };
        }

        protected virtual Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}