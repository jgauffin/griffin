using System;
using System.Net;
using System.Net.Sockets;
using Griffin.Core;
using Griffin.Networking.Messages;
using Griffin.Networking.Pipelines;

namespace Griffin.Networking.Channels
{
    public class TcpClientChannel : TcpChannel
    {
        private EndPoint _remoteEndPoint;

        public TcpClientChannel(IPipeline pipeline) : base(pipeline)
        {
        }

        protected override void DownstreamHandlingComplete(IChannelEvent e)
        {
            if (e is BindEvent)
            {
                _remoteEndPoint = e.As<BindEvent>().EndPoint;
                HandlerContexts.SendUpstream(new BoundEvent());
            }
            if (e is ConnectEvent)
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.BeginConnect(_remoteEndPoint, OnConnect, socket);
            }
            else
                base.DownstreamHandlingComplete(e);
        }

        private void OnConnect(IAsyncResult ar)
        {
            var socket = ar.AsyncState.As<Socket>();
            try
            {
                socket.EndConnect(ar);
                Socket = socket;
                HandlerContexts.SendUpstream(new ConnectedEvent(socket.LocalEndPoint, socket.RemoteEndPoint));
                StartRead();
            }
            catch (Exception err)
            {
                socket.Dispose();
                ReportException(err);
            }
        }
    }
}