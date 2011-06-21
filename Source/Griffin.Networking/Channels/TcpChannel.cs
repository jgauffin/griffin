using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Griffin.Core.Net.Buffers;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Messages;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.Channels
{
    public abstract class TcpChannel : Channel
    {
        private TcpChannelConfig _channelConfig;
        private bool _isSending;
        private EndPoint _localEndPoint;
        private IPipeline _pipeline;
        private SocketAsyncEventArgs _readArgs;
        private EndPoint _remoteEndPoint;
        private SocketAsyncEventArgs _sendArgs;
        private ConcurrentQueue<BufferSlice> _sendQueue = new ConcurrentQueue<BufferSlice>();
        private BufferSlice _readSlice;

        protected TcpChannel(IPipeline pipeline)
            : base(pipeline)
        {
        }

        public bool IsSharable
        {
            get { return false; }
        }

        protected Socket Socket { get; set; }

        private void Close(SocketError error)
        {
            try
            {
                Socket.Shutdown(SocketShutdown.Send);
            }
            // throws if client process has already closed
            catch
            {
            }
            Socket.Close();


            _channelConfig.BufferManager.FreeBuffer(_readArgs);
            _channelConfig.SocketAsyncEventPool.Push(_readArgs);
            _channelConfig.SocketAsyncEventPool.Push(_sendArgs);
            SendUpstream(new SocketClosedEvent(_localEndPoint, _remoteEndPoint, error));
        }

        protected override void DownstreamHandlingComplete(IChannelEvent e)
        {
            if (e is MessageEvent)
            {
                var evt = e.As<MessageEvent>();
                var buffer = evt.Message.As<BufferSlice>();
                Send(buffer);
            }
            else if (e is CloseEvent)
            {
                Close(SocketError.Success);
            }
        }

        protected override void UpstreamHandlingComplete(IChannelEvent e)
        {
            _readSlice.MoveRemainingBytes();
            //var bytesRemaining = _inbufferStream.Length - _inbufferStream.Position;
            // copy the remaining bytes to the start of buffer
            //Buffer.BlockCopy(_readArgs.Buffer, (int)_inbufferStream.Position, _readArgs.Buffer, 0, (int)bytesRemaining);
            //_inbufferStream.SetLength(bytesRemaining);

            StartRead();
        }


        public virtual void Initialize(TcpChannelConfig config)
        {
            Socket = config.Socket;
            _remoteEndPoint = Socket.RemoteEndPoint;
            _localEndPoint = Socket.LocalEndPoint;
            _channelConfig = config;
            _sendArgs = config.SocketAsyncEventPool.Pull();
            _sendArgs.Completed += WriteCompleted;
            _readArgs = config.SocketAsyncEventPool.Pull();
            _readArgs.Completed += ReadCompleted;
            _readSlice = config.BufferManager.AssignBufferTo(_readArgs);
            _readSlice.Count = 0;
            //_inbufferStream = new MemoryStream(_readArgs.Buffer, _readArgs.Offset, _readArgs.Count);
            //_inbufferStream.SetLength(0);

            Initialize();
        }

        private int count = 0;
        private void ReadCompleted(object sender, SocketAsyncEventArgs e)
        {
            SocketError error = e.BytesTransferred == 0 ? SocketError.ConnectionReset : e.SocketError;
            Console.WriteLine("Read " + e.BytesTransferred);
            if (error != SocketError.Success)
            {
                ++count;
                if (count == 1)
                    StartRead();
                SendUpstream(new SocketClosedEvent(_localEndPoint, _remoteEndPoint, error));
                return;
            }

            try
            {
                _readSlice.Count += e.BytesTransferred;
                Console.WriteLine("Read total " + _readSlice.Count);

                int oldBytes = 0;
                int bytesLeft = _readSlice.Count;
                while (bytesLeft != 0 && bytesLeft != oldBytes)
                {
                    SendUpstream(new MessageEvent(_readSlice));
                    oldBytes = bytesLeft;
                    _readSlice.MoveRemainingBytes();
                    bytesLeft = _readSlice.Count;
                }
            }
            catch (Exception err)
            {
                ReportException(err);
            }

            try
            {
                StartRead();
            }
            catch (Exception err)
            {
                ReportException(err);
            }

            Console.WriteLine("REad completed done");
        }

        protected void ReportException(Exception exception)
        {
            if (exception is SocketException)
            {
                var ex = exception.As<SocketException>();
                SendUpstream(new SocketClosedEvent(_localEndPoint, _remoteEndPoint, ex.SocketErrorCode));
                return;
            }

            SendUpstream(new ExceptionEvent(exception));
        }

        public void Send(BufferSlice bufferSlice)
        {
            if (bufferSlice == null)
                throw new ArgumentNullException("bufferSlice");

            _sendQueue.Enqueue(bufferSlice);
            if (_isSending)
                return;

            lock (_sendQueue)
            {
                if (_isSending)
                    return;
                _isSending = true;
            }

            SendFirstBuffer();
        }

        protected virtual void SendFirstBuffer()
        {
            BufferSlice buffer;
            if (!_sendQueue.TryDequeue(out buffer))
            {
                _isSending = false;
                return;
            }

            _sendArgs.SetBuffer(buffer.Buffer, buffer.AssignedOffset, buffer.Count);

            Console.WriteLine("Sending");
            if (!Socket.SendAsync(_sendArgs))
                WriteCompleted(this, _sendArgs);
        }

        private void SendUpstream(IChannelEvent evt)
        {
            HandlerContexts.SendUpstream(evt);
            //var ctx = new ChannelHandlerContext(this, _pipeline);
            //ctx.SendUpstream(evt);
        }

        protected void StartRead()
        {
            try
            {
                /*
                _readSlice.MoveRemainingBytes();
                if (_readSlice.CurrentOffset != _readSlice.AssignedOffset)
                    _readArgs.SetBuffer(_readSlice.CurrentOffset, _readSlice.AllocatedCount - _readSlice.Count);
                Console.WriteLine("* Reading from offset: " + _readArgs.Offset + " and count: " + _readArgs.Count);
                 */
                if (!Socket.ReceiveAsync(_readArgs))
                {
                    Console.WriteLine("completed directoy");
                    ReadCompleted(this, _readArgs);
                }
            }
            catch (Exception err)
            {
                ReportException(err);
            }
            Console.WriteLine("* Reading done");
        }

        private void WriteCompleted(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine("Sent");
            if (e.SocketError != SocketError.Success)
            {
                SendUpstream(new SocketClosedEvent(_localEndPoint, _remoteEndPoint, e.SocketError));
                Close(e.SocketError);
                return;
            }

            SendFirstBuffer();
            Console.WriteLine("Sent done");
        }
    }
}