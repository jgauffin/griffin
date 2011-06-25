using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Griffin.Core;
using Griffin.Networking.Buffers;
using Griffin.Networking.Channels;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;
using Griffin.Networking.Pipelines;
using Xunit;

namespace Griffin.Networking.Tests.Channels
{
    public class TcpChannelTest : IUpstreamHandler, IDownstreamHandler
    {
        private readonly BufferManager _bufferManager = new BufferManager(5, 65535);
        private readonly TcpChannelWrapper _channel;
        private readonly TcpClientHelper _helper;
        private readonly Pipeline _pipeline = new Pipeline();

        private readonly ObjectPool<SocketAsyncEventArgs> _pool =
            new ObjectPool<SocketAsyncEventArgs>(() => new SocketAsyncEventArgs());

        private readonly ManualResetEvent _receiveTrigger = new ManualResetEvent(false);

        private readonly List<BufferSlice> _receivedBuffers = new List<BufferSlice>();
        private readonly List<IChannelEvent> _receuvedEvents = new List<IChannelEvent>();
        private Type _wantedReceiveEvent = typeof (MessageEvent);

        public TcpChannelTest()
        {
            _channel = new TcpChannelWrapper(_pipeline);
            _helper = new TcpClientHelper();
            _bufferManager.CreateBuffers();

            _pipeline.RegisterUpstreamHandler(this);
            _pipeline.RegisterDownstreamHandler(this);

            Socket socket = _helper.GetConnectedSocket();
            var config = new TcpChannelConfig(_bufferManager, _pool) {Socket = socket};
            _channel.Initialize(config);
        }

        /// <summary>
        /// Gets if this pipeline can be shared between multiple channels
        /// </summary>
        /// <remarks>
        /// Return <c>false</c> if you have member variables.
        /// </remarks>
        public bool IsSharable
        {
            get { return true; }
        }

        #region IDownstreamHandler Members

        /// <summary>
        /// Handle the data that is going to be sent to the remote end point
        /// </summary>
        /// <param name="ctx">Context information</param>
        /// <param name="e">Chennel event.</param>
        public void HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            ctx.SendDownstream(e);
        }

        #endregion

        #region IUpstreamHandler Members

        /// <summary>
        /// Process data that was received from the channel.
        /// </summary>
        /// <param name="ctx">Context which is used for the currently processed channel</param>
        /// <param name="e">Event being processed.</param>
        public void HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            if (e is MessageEvent)
                _receivedBuffers.Add(e.As<MessageEvent>().Message.As<BufferSlice>());

            _receuvedEvents.Add(e);
            if (_wantedReceiveEvent.IsAssignableFrom(e.GetType()))
                _receiveTrigger.Set();
        }

        #endregion

        [Fact]
        public void TestSendDirectly()
        {
            byte[] buffer = Encoding.ASCII.GetBytes("Hello world");
            var slice = new BufferSlice(buffer, 0, buffer.Length);
            _channel.Send(slice);

            Assert.True(_helper.WaitForReceive(1000), "Did not receive anything");

            BufferSlice firstBuffer = _helper.RecievedBuffers.First();
            Assert.Equal("Hello world", Encoding.ASCII.GetString(firstBuffer.Buffer, 0, firstBuffer.Count));
        }

        [Fact]
        public void TestSendThroughPipeline()
        {
            byte[] buffer = Encoding.ASCII.GetBytes("Hello world");
            var slice = new BufferSlice(buffer, 0, buffer.Length);
            _pipeline.SendDownstream(_channel, new MessageEvent(slice));

            Assert.True(_helper.WaitForReceive(1000), "Did not receive anything");

            BufferSlice firstBuffer = _helper.RecievedBuffers.First();
            Assert.Equal("Hello world", Encoding.ASCII.GetString(firstBuffer.Buffer, 0, firstBuffer.Count));
        }

        [Fact]
        public void TestReceiveFromPipeline()
        {
            _helper.Send(Encoding.ASCII.GetBytes("omg!"));
            WaitFor<MessageEvent>(500);
            BufferSlice buffer = _receivedBuffers.First();
            var reader = new BufferSliceReader(buffer);
            Assert.Equal("omg!", reader.ReadToEnd());
        }

        [Fact]
        public void TestDisconnect()
        {
            _helper.Close();
            Assert.NotNull(WaitFor<ClosedEvent>(50000));
        }

        private T WaitFor<T>(int timeout) where T : class, IChannelEvent
        {
            _wantedReceiveEvent = typeof (T);
            if (!_receiveTrigger.WaitOne(timeout))
                return null;

            return (T) _receuvedEvents.FirstOrDefault(e => e is T);
        }

        #region Nested type: TcpChannelWrapper

        private class TcpChannelWrapper : TcpChannel
        {
            public TcpChannelWrapper(IPipeline pipeline)
                : base(pipeline)
            {
            }
        }

        #endregion
    }
}