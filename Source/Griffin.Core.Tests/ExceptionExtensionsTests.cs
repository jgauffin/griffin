using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Griffin.Core.Tests
{
    public class ExceptionExtensionsTests
    {
        [Fact]
        public void PreserveStackTrace()
        {
            try
            {
                throw new ArgumentException("Woot!");
            }
            catch(Exception err)
            {
                var trace = err.StackTrace;
                err.PreserveStacktrace();
                try
                {
                    throw err;
                }
                catch (Exception)
                {
                    Assert.Contains(trace, err.StackTrace);
                }
            }
        }
    }
}
