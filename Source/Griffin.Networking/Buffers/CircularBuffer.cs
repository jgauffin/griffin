using System;

namespace Griffin.Networking.Buffers
{
    internal class CircularBuffer
    {
        private readonly byte[] _buffer;
        private readonly int _capacity;
        private int _head;
        private int _size;
        private int _tail;

        public CircularBuffer(int capacity)
        {
            _buffer = new byte[capacity];
            _capacity = capacity;
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public int Size
        {
            get { return _size; }
        }

        public int Get(byte[] destination, int offset, int count)
        {
            int trueCount = Math.Min(count, Size);
            for (int i = 0; i < trueCount; ++i)
                destination[offset + i] = _buffer[(_head + i)%_capacity];
            _head += trueCount;
            _head %= _capacity;
            _size -= trueCount;
            return trueCount;
        }

        public int Get()
        {
            if (Size == 0)
                return -1;

            int result = _buffer[_head++%_capacity];
            --_size;
            return result;
        }

        public int Put(byte[] source, int offset, int count)
        {
            int trueCount = Math.Min(count, _capacity - Size);
            for (int i = 0; i < trueCount; ++i)
                _buffer[(_tail + i)%_capacity] = source[offset + i];
            _tail += trueCount;
            _tail %= _capacity;
            _size += trueCount;
            return trueCount;
        }

        public bool Put(byte b)
        {
            if (Size == _capacity) // no room
                return false;
            _buffer[_tail++] = b;
            _tail %= _capacity;
            ++_size;
            return true;
        }
    }
}