using System;
using System.Text;

namespace Griffin.Core.Net.Buffers
{
    /// <summary>
    /// Part of a larger buffer
    /// </summary>
    /// <remarks>
    /// A larger buffer can be allocated and split instead of allocating a lot of small
    /// buffers to prevent defragmentation and to improve performance.
    /// </remarks>
    public class BufferSlice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BufferSlice"/> class.
        /// </summary>
        /// <param name="buffer">The whole buffer.</param>
        /// <param name="offset">Offset assigned to this slice.</param>
        /// <param name="count">Number of bytes assigned.</param>
        public BufferSlice(byte[] buffer, int offset, int count)
        {
            Buffer = buffer;
            AllocatedCount = count;
            AssignedOffset = offset;
            CurrentOffset = offset;
            Count = count;
        }

        /// <summary>
        /// Gets entire buffer
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// Gets number of bytes allocated to this slice
        /// </summary>
        public int AllocatedCount { get; private set; }

        /// <summary>
        /// Gets the offset allocated to this slice
        /// </summary>
        public int AssignedOffset { get; private set; }

        /// <summary>
        /// Gets index after the last byte
        /// </summary>
        public int EndOffset { get { return AssignedOffset + AllocatedCount; } }

        private int _currentOffset;

        /// <summary>
        /// Gets or sets current position in he buffer
        /// </summary>
        public int CurrentOffset
        {
            get { return _currentOffset; }
            set
            {
                if (value < AssignedOffset || value >= AssignedOffset + AllocatedCount)
                    throw new IndexOutOfRangeException("Current offset is out of range");

                _currentOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets number of bytes that contains data
        /// </summary>
        public  int Count { get; set; }

        /// <summary>
        /// Gets the number of written bytes remaining in our slice.
        /// </summary>
        public int RemainingCount { get { return Count - (CurrentOffset -  AssignedOffset); } }

        /// <summary>
        /// Move all not processed bytes to the beginning of the array
        /// </summary>
        public void MoveRemainingBytes()
        {
            var bytes = RemainingCount;
            Console.WriteLine("Remainging bytes: " + bytes);
            System.Buffer.BlockCopy(Buffer, CurrentOffset, Buffer, AssignedOffset, RemainingCount);
            Count = bytes;
            Console.WriteLine("Setting current offset: " + (AssignedOffset + bytes));
            CurrentOffset = AssignedOffset + bytes;
            if (bytes > 0)
            {
                Console.WriteLine(Encoding.ASCII.GetString(Buffer, AssignedOffset, bytes));
            }
        }
    }
}