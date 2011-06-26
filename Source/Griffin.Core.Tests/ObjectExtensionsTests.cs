using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Griffin.Core.Tests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void TestInvalidCast()
        {
            Assert.Throws(typeof (InvalidCastException), () => "abc".As<int>());
        }

        [Fact]
        public void TestValidCast()
        {
            object abc = 11;
            Assert.Equal(11, abc.As<int>());
            Assert.IsType(typeof (int), abc.As<int>());
        }

    }
}
