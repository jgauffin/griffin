using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Griffin.Core
{
    /// <summary>
    /// Class used to be able to handle exceptions in worker threads.
    /// </summary>
    public static class ExceptionHandler
    {
        private static int _mainThread;

        /// <summary>
        /// Gets id of the main application thread
        /// </summary>
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

        /// <summary>
        /// We got an exception that is unhandled.
        /// </summary>
        /// <param name="source">Object that the exception was caught in.</param>
        /// <param name="exception">Actual exception</param>
        /// <returns>What to do</returns>
        public static ExceptionPolicy GotUnhandled(object source, Exception exception)
        {
            return GetCurrentThreadId() == MainThreadId
                       ? ExceptionPolicy.Throw
                       : ThrownInThread(source, exception);
        }

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

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
}