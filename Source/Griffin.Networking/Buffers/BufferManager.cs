using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;

namespace Griffin.Networking.Buffers
{
    public class BufferManager
    {
        private readonly ConcurrentStack<int> _freeIndexes;
        private readonly int _returnedBufferSize;
        private readonly int _sizeOfEntireBuffer;
        private byte[] _buffer;

        public BufferManager(int maxNumberOfClients, int bufferSize)
        {
            _sizeOfEntireBuffer = maxNumberOfClients*bufferSize;
            _returnedBufferSize = bufferSize;
            _freeIndexes = new ConcurrentStack<int>();
        }

        /// <summary>
        /// Assigns a buffer from the buffer pool to the specified SocketAsyncEventArgs object
        /// </summary>
        /// <returns>true if the buffer was successfully set; otherwise false</returns>
        public BufferSlice AssignBufferTo(SocketAsyncEventArgs args)
        {
            int index = GetIndex();
            args.SetBuffer(_buffer, index, _returnedBufferSize);
            return new BufferSlice(_buffer, index, _returnedBufferSize);
        }

        /// <summary>
        /// Removes the buffer from a SocketAsyncEventArg object.  This frees the buffer back to the 
        /// buffer pool
        /// </summary>
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            _freeIndexes.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

        private int GetIndex()
        {
            int attempt = 0;
            while (attempt < 5)
            {
                int result = -1;
                if (_freeIndexes.TryPop(out result))
                    return result;

                Thread.Sleep(1);
                ++attempt;
            }

            throw new InvalidOperationException("Was asked to get another memory buffer, but none was available.");
        }

        /// <summary>
        /// Allocates buffer space used by the buffer pool
        /// </summary>
        public void CreateBuffers()
        {
            _buffer = new byte[_sizeOfEntireBuffer];
            for (int i = 0; i < _sizeOfEntireBuffer; i += _returnedBufferSize)
                _freeIndexes.Push(i);
        }
    }
}