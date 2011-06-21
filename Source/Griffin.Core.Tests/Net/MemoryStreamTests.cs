using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Griffin.Core.Tests.Net
{
    public class MemoryStreamTests
    {
        byte[] _buffer = new byte[65535];
        private MemoryStream _stream;

        public MemoryStreamTests()
        {
            _stream = new MemoryStream(_buffer, 8192, 8192);
            _stream.SetLength(0);
        }

        [Fact]
        public void Test()
        {
        }

    }
}
