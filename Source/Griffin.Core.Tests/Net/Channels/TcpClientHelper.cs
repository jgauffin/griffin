using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Griffin.Core.Net.Buffers;

namespace Griffin.Core.Tests.Net.Channels
{
    public class TcpClientHelper
    {
        private Socket _socket;
        List<BufferSlice> _receivedBuffers = new List<BufferSlice>();
        ManualResetEvent _readEvent = new ManualResetEvent(false);

        public void AcceptSocket()
        {
            
        }

        public List<BufferSlice> RecievedBuffers { get { return _receivedBuffers; } }
        public bool WaitForReceive(int ms)
        {
            var res = _readEvent.WaitOne(ms);
            _readEvent.Reset();
            return res;
        }

        public void Send(byte[] buffer)
        {
            _socket.Send(buffer);
        }

        public Socket GetConnectedSocket()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var res=listener.BeginAcceptSocket(null, null);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(IPAddress.Loopback, listener.LocalEndpoint.As<IPEndPoint>().Port);
            var clientSocket =listener.EndAcceptSocket(res);

            StartRead();
            return clientSocket;
        }

        private void StartRead()
        {
            byte[] buffer = new byte[65535];
            _socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnRecieve, buffer);
        }

        private void OnRecieve(IAsyncResult ar)
        {
            var read = _socket.EndReceive(ar);
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
