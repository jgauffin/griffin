using System;
using System.Linq;
using System.Net;
using System.Threading;
using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Messages;

namespace Griffin.Core.Net.Services
{
    public abstract class ClientService : SimpleUpstreamHandler
    {
        private IChannel _channel;
        Timer _reconnectTimer;

        /// <summary>
        /// Span to set 
        /// </summary>
        public static readonly TimeSpan Disabled = new TimeSpan(0,0,0,0,-1);

        protected ClientService()
        {
            ReconnectInterval = Disabled;
        }

        private EndPoint _localEndPoint = new IPEndPoint(IPAddress.Any, 0);
        public virtual EndPoint LocalEndPoint
        {
            get { return _localEndPoint; }
            set { _localEndPoint = value; }
        }

        /// <summary>
        /// Gets or sets how often a reconnect attempt should be made
        /// </summary>
        /// <value>
        /// Default is <see cref="Disabled"/>.
        /// </value>
        public TimeSpan ReconnectInterval { get; set; }

        protected override void HandleClosed(IChannelHandlerContext ctx, ClosedEvent e)
        {
            if (ReconnectInterval == Disabled)
                return;

            // Always wait for closed event (it will also be signalled when Connect event fails)
            if (_reconnectTimer == null)
                _reconnectTimer = new Timer(OnTryConnect, null, ReconnectInterval, Disabled);
            else
                _reconnectTimer.Change(ReconnectInterval, Disabled);
        }

        private void OnTryConnect(object state)
        {
            if (ReconnectInterval == Disabled)
                return;

            _reconnectTimer.Change(Disabled, Disabled);
            SendDownstream(new ConnectEvent());
        }

        protected override void HandleConnected(IChannelHandlerContext ctx, ConnectedEvent e)
        {
            if (_reconnectTimer != null)
            {
                _reconnectTimer.Dispose();
                _reconnectTimer = null;
            }
        }
        public void Close()
        {
            var evt = new CloseEvent();
            SendDownstream(evt);
        }

        

        protected override void HandleBound(IChannelHandlerContext ctx, BoundEvent e)
        {
            var evt = new ConnectEvent();
            SendDownstream(evt);
        }

        public void Connect(IChannel channel, EndPoint remoteEndPoint)
        {
            ConfigureChannel(channel);
            var bindEvent = new BindEvent(remoteEndPoint, false);
            SendDownstream(bindEvent);
        }

        public void SendDownstream(IChannelEvent channelEvent)
        {
            _channel.HandlerContexts.SendDownstream(channelEvent);
        }

        private void ConfigureChannel(IChannel channel)
        {
            if (_channel == channel)
                return;

            _channel = channel;
        }
    }
}