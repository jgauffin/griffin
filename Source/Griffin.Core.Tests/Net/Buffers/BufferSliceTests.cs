using System;
using System.Text;
using Griffin.Core.Net.Buffers;
using Xunit;

namespace Griffin.Core.Tests.Net.Buffers
{
    public class BufferSliceTests
    {
     
        [Fact]
        public void Test()
        {
            var buffer = new byte[65535];
            var hello = Encoding.ASCII.GetBytes("Hello world!");
            Buffer.BlockCopy(hello, 0, buffer, 1024, hello.Length);
            BufferSlice slice = new BufferSlice(buffer, 1024, 1024);
            slice.Count = hello.Length;

            Assert.Equal(hello.Length, slice.RemainingCount);
            Assert.Equal(hello.Length, slice.Count);

            slice.CurrentOffset += 1;
            Assert.Equal(hello.Length-1, slice.RemainingCount);
            Assert.Equal(hello.Length, slice.Count);
        }
    }
}
