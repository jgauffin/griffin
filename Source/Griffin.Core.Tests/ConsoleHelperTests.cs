using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Griffin.Core.Tests
{

    public class ConsoleHelperTests
    {
        [Fact]
        public void TestGotConsole()
        {
            //TODO: Unit tests seems to create a console.
            // we need to figure out how to test the code.
            //Assert.False(ConsoleHelper.HasConsole);
        }

        [Fact]
        public void TestCreateConsole()
        {
            ConsoleHelper.CreateConsole();
            Assert.True(ConsoleHelper.HasConsole);
        }
    }
}
