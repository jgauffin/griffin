using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Griffin.Core;
using Griffin.Networking.Buffers;

namespace Griffin.Networking.Tests.Channels
{
    public class TcpClientHelper
    {
        private readonly ManualResetEvent _readEvent = new ManualResetEvent(false);
        private readonly List<BufferSlice> _receivedBuffers = new List<BufferSlice>();
        private Socket _socket;

        public List<BufferSlice> RecievedBuffers
        {
            get { return _receivedBuffers; }
        }

        public void AcceptSocket()
        {
        }

        public bool WaitForReceive(int ms)
        {
            bool res = _readEvent.WaitOne(ms);
            _readEvent.Reset();
            return res;
        }

        public void Send(byte[] buffer)
        {
            _socket.Send(buffer);
        }

        public Socket GetConnectedSocket()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            IAsyncResult res = listener.BeginAcceptSocket(null, null);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(IPAddress.Loopback, listener.LocalEndpoint.As<IPEndPoint>().Port);
            Socket clientSocket = listener.EndAcceptSocket(res);

            StartRead();
            return clientSocket;
        }

        private void StartRead()
        {
            var buffer = new byte[65535];
            _socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnRecieve, buffer);
        }

        private void OnRecieve(IAsyncResult ar)
        {
            int read = _socket.EndReceive(ar);
            if (read == 0)
                return;

            var buffer = (byte[]) ar.AsyncState;
            _receivedBuffers.Add(new BufferSlice(buffer, 0, read));

            StartRead();
            _readEvent.Set();
        }

        public void Close()
        {
            _socket.Close();
        }
    }
}