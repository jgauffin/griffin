using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Griffin.Core.Net.Buffers;
using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Messages;
using Griffin.Core.Net.Pipelines;
using Xunit;

namespace Griffin.Core.Tests.Net.Channels
{
    public class TcpChannelTest : IUpstreamHandler, IDownstreamHandler
    {
        private TcpChannelWrapper _channel;
        private Pipeline _pipeline = new Pipeline();
        private BufferManager _bufferManager = new BufferManager(5, 65535);
        private TcpClientHelper _helper;
        private ObjectPool<SocketAsyncEventArgs> _pool = new ObjectPool<SocketAsyncEventArgs>(() => new SocketAsyncEventArgs());
        List<BufferSlice> _receivedBuffers = new List<BufferSlice>();
        List<IChannelEvent> _receuvedEvents = new List<IChannelEvent>();
        private ManualResetEvent _receiveTrigger = new ManualResetEvent(false);
        private Type _wantedReceiveEvent = typeof (MessageEvent);

        private class TcpChannelWrapper : TcpChannel
        {
            public TcpChannelWrapper(IPipeline pipeline)
                : base(pipeline)
            {
            }
        }

        public TcpChannelTest()
        {
            _channel = new TcpChannelWrapper(_pipeline);
            _helper=new TcpClientHelper();
            _bufferManager.CreateBuffers();

            _pipeline.RegisterUpstreamHandler(this);
            _pipeline.RegisterDownstreamHandler(this);

            var socket = _helper.GetConnectedSocket();
            var config = new TcpChannelConfig(_bufferManager, _pool) { Socket = socket };
            _channel.Initialize(config);

        }

        [Fact]
        public void TestSendDirectly()
        {
            var buffer = Encoding.ASCII.GetBytes("Hello world");
            var slice = new BufferSlice(buffer, 0, buffer.Length);
            _channel.Send(slice);

            Assert.True(_helper.WaitForReceive(1000), "Did not receive anything");

            var firstBuffer = _helper.RecievedBuffers.First();
            Assert.Equal("Hello world", Encoding.ASCII.GetString(firstBuffer.Buffer, 0, firstBuffer.Count));
        }

        [Fact]
        public void TestSendThroughPipeline()
        {
            var buffer = Encoding.ASCII.GetBytes("Hello world");
            var slice = new BufferSlice(buffer, 0, buffer.Length);
            _pipeline.SendDownstream(_channel, new MessageEvent(slice));

            Assert.True(_helper.WaitForReceive(1000), "Did not receive anything");

            var firstBuffer = _helper.RecievedBuffers.First();
            Assert.Equal("Hello world", Encoding.ASCII.GetString(firstBuffer.Buffer, 0, firstBuffer.Count));
            
        }

        [Fact]
        public void TestReceiveFromPipeline()
        {
            _helper.Send(Encoding.ASCII.GetBytes("omg!"));
            WaitFor<MessageEvent>(500);
            var buffer = _receivedBuffers.First();
            var reader = new BufferSliceReader(buffer);
            Assert.Equal("omg!", reader.ReadToEnd());
        }

        [Fact]
        public void TestDisconnect()
        {
            _helper.Close();
            Assert.NotNull(WaitFor<ClosedEvent>(50000));
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

        /// <summary>
        /// Handle the data that is going to be sent to the remote end point
        /// </summary>
        /// <param name="ctx">Context information</param>
        /// <param name="e">Chennel event.</param>
        public void HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            ctx.SendDownstream(e);
        }


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

        private T WaitFor<T>(int timeout) where T: class, IChannelEvent
        {
            _wantedReceiveEvent = typeof (T);
            if (!_receiveTrigger.WaitOne(timeout))
                return null;

            return (T)_receuvedEvents.FirstOrDefault(e => e is T);
        }
    }
}
