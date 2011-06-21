using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Griffin.Core
{
    public static class UnhandledException
    {
        public static void TriggerEvent(object source, string message, Exception exception)
        {
            
        }
    }
    public static class ExceptionHandler
    {
        private static int _mainThread = 0;

        public static int MainThreadId
        {
            get
            {
                if (_mainThread != 0)
                    return _mainThread;

                _mainThread = GetStartThread();
                return _mainThread;
            }
        }
        public static ExceptionPolicy GotUnhandled(object source, Exception exception)
        {
            return GetCurrentThreadId() == MainThreadId
                       ? ExceptionPolicy.Throw
                       : ThrownInThread(source, exception);
        }

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        private static int GetStartThread()
        {
            int threadId = 0;
            DateTime startTime = DateTime.MaxValue;
            foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
            {
                if (thread.StartTime < startTime)
                {
                    threadId = thread.Id;
                    startTime = thread.StartTime;
                }

            }
            return threadId;
        }

        public static ExceptionPolicy ThrownInThread(object source, Exception err)
        {
            return ExceptionPolicy.Ignore;
        }

        public static void TriggerUnhandledException(object source, UnhandledExceptionEventArgs args)
        {
            
        }

        public static event EventHandler UnhandledException = delegate { };
    }

    public class UnhandledExceptionEventArgs:EventArgs
    {
        public UnhandledExceptionEventArgs(string message, Exception unhandledException)
        {
            
        }

    }
    public enum ExceptionPolicy
    {
        Throw,
        Ignore
    }
}
