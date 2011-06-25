using System;
using System.Net;
using System.Net.Sockets;
using Griffin.Core;
using Griffin.Networking.Buffers;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;
using Griffin.Networking.Pipelines;

namespace Griffin.Networking.Channels
{
    /// <summary>
    /// TCP server.
    /// </summary>
    public class TcpServerChannel : IChannel
    {
        private readonly ObjectPool<SocketAsyncEventArgs> _argsPool;
        private readonly BufferManager _bufferManager;
        private readonly ContextCollection _contexts;
        private TcpListener _listener;

        public TcpServerChannel(IPipeline serverPipeline, IPipeline childPipeline, int maxNumberOfClients)
        {
            _bufferManager = new BufferManager(maxNumberOfClients, 65535);
            _argsPool = new ObjectPool<SocketAsyncEventArgs>(AllocateArgs);
            Pipeline = serverPipeline;
            _contexts = new ContextCollection(this);
            ChildPipeline = childPipeline;
        }

        /// <summary>
        /// Gets the pipeline that child channels will use.
        /// </summary>
        public IPipeline ChildPipeline { get; private set; }

        #region IChannel Members

        /// <summary>
        /// Process data that was received from the channel.
        /// </summary>
        /// <param name="ctx">Context which is used for the currently processed channel</param>
        /// <param name="e">Event being processed.</param>
        public void HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
        }

        /// <summary>
        /// Handle the data that is going to be sent to the remote end point
        /// </summary>
        /// <param name="ctx">Context information</param>
        /// <param name="e">Chennel event.</param>
        public void HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            if (e is BindEvent)
            {
                if (_listener != null)
                    throw new InvalidOperationException("Listener have already been specified.");

                _listener = new TcpListener(e.As<BindEvent>().EndPoint.As<IPEndPoint>());
                _listener.Start(1000);
                _listener.BeginAcceptSocket(OnAcceptSocket, null);
            }
            else if (e is CloseEvent)
            {
                _listener.Stop();
                _listener = null;
            }
        }

        /// <summary>
        /// Gets pipeline that this channel is attached to.
        /// </summary>
        public IPipeline Pipeline { get; private set; }

        /// <summary>
        /// Gets contexts used for each handler in a channel.
        /// </summary>
        public IContextCollection HandlerContexts
        {
            get { return _contexts; }
        }

        #endregion

        private SocketAsyncEventArgs AllocateArgs()
        {
            return new SocketAsyncEventArgs();
        }


        private void HandleException(Exception err)
        {
            SendUpstream(new ExceptionEvent(err));
        }

        private void OnAcceptSocket(IAsyncResult ar)
        {
            try
            {
                Socket socket = _listener.EndAcceptSocket(ar);
                var client = new TcpServerChildChannel(ChildPipeline);

                var config = new TcpChannelConfig(_bufferManager, _argsPool) {Socket = socket};
                client.Initialize(config);
                SendUpstream(new AcceptedEvent {ClientChannel = client});
            }
            catch (Exception err)
            {
                HandleException(err);
            }
        }

        private void SendUpstream(IChannelEvent channelEvent)
        {
            HandlerContexts.SendUpstream(channelEvent);
        }
    }
}