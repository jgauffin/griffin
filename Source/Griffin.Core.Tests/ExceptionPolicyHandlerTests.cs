using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Griffin.Core.Tests
{
    public class ExceptionPolicyHandlerTests
    {
        [Fact]
        public void TestWithoutSubscribers()
        {
            ExceptionPolicyHandler.GotUnhandled(this, new Exception());
        }

        [Fact]
        public void TestThrownInWorkerThread()
        {
            Exception ex = null;
            ExceptionPolicyHandler.UnhandledException += delegate(object o, UnhandledExceptionEventArgs e)
                                                             {
                                                                 ex = e.UnhandledException;
                                                                 e.ActionToTake = ExceptionPolicy.Throw;
                                                             };

            var realException = new Exception();
            var policy = ExceptionPolicyHandler.GotUnhandled(this, realException);
            Assert.Equal(realException, ex);
            Assert.Equal(ExceptionPolicy.Throw, policy);
        }

        [Fact]
        public void TestThrownInMainThread()
        {
            return;

            //TODO: Need to find a way to run in the main thread.

            Exception ex = null;
            ExceptionPolicyHandler.UnhandledException += delegate(object o, UnhandledExceptionEventArgs e)
            {
                ex = e.UnhandledException;
                e.ActionToTake = ExceptionPolicy.Ignore;
            };

            var realException = new Exception();
            var policy = ExceptionPolicyHandler.GotUnhandled(this, realException);
            Assert.Equal(realException, ex);
            Assert.Equal(ExceptionPolicy.Throw, policy);
        }
    }
}
