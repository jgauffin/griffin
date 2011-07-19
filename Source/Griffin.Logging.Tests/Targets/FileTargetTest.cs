using System;
using System.Diagnostics;
using System.Threading;
using Griffin.Logging.Targets.File;
using Xunit;

namespace Griffin.Logging.Tests.Targets
{
    public class FileTargetTest : IFileWriter
    {
        private readonly FileTarget _target;

        public FileTargetTest()
        {
            _target = new FileTarget(this);
        }

        #region IFileWriter Members

        public FileConfiguration Configuration
        {
            get
            {
                return new FileConfiguration
                           {
                               CreateDateFolder = true,
                               Path = @"C:\Temp\",
                               DaysToKeep = 1
                           };
            }
        }

        public string Name
        {
            get { return "TestLog"; }
        }

        public void Write(string logEntry)
        {
            throw new NotImplementedException();
        }

        #endregion

        [Fact]
        public void TestEntry()
        {
            _target.Enqueue(new LogEntry
                                {
                                    CreatedAt = DateTime.Now,
                                    LogLevel = LogLevel.Warning,
                                    Message = "Hello world",
                                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                                    UserName = Environment.UserName,
                                    StackFrames = new StackTrace(2).GetFrames()
                                });
        }
    }
}