using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Griffin.Core.Tests
{
    public class CollectionExtensionTests
    {
        [Fact]
        public void TestEmptyCollection()
        {
            var items = new List<string>();
            int counter = 0;
            items.Each(item => ++counter);
            Assert.Equal(0, counter);
        }

        [Fact]
        public void TestNullCollection()
        {
            List<string> items = null;
            int counter = 0;
            items.Each(item => ++counter);
            Assert.Equal(0, counter);
        }

        [Fact]
        public void TestOneItem()
        {
            List<string> items = new List<string>{"a"};
            int counter = 0;
            items.Each(item =>
                              {
                                  counter++;
                                  Assert.Equal("a", item);
                              });
            Assert.Equal(1, counter);
        }

        [Fact]
        public void TestTwoItems()
        {
            List<string> items = new List<string> { "a", "b" };
            int counter = 0;
            items.Each(item =>
            {
                counter++;
                switch (counter)
                {
                    case 1:
                        Assert.Equal("a", item);
                        break;
                    case 2:
                        Assert.Equal("b", item);
                        break;
                }
            });
            Assert.Equal(2, counter);
        }

        [Fact]
        public void TestNull()
        {
            var items = new List<string>();
            Assert.Throws(typeof (ArgumentNullException), () => items.Each(null));

        }
    }
}
