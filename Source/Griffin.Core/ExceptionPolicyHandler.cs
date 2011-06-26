using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Griffin.Core
{
    /// <summary>
    /// Class used to be able to handle uncaught exceptions in worker threads.
    /// </summary>
    /// <remarks>
    /// Do NOT use this policy handler in your general exception handling. It's intended purpose is
    /// only to decide wether unhandled exceptions should be able to terminate your application
    /// or to keep it running.
    /// </remarks>
    public static class ExceptionPolicyHandler
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

        /// <summary>
        /// We got an exception that is unhandled.
        /// </summary>
        /// <param name="source">Object that the exception was caught in.</param>
        /// <param name="exception">Actual exception</param>
        /// <returns>What to do</returns>
        public static ExceptionPolicy GotUnhandled(object source, Exception exception)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(exception != null);

            return GetCurrentThreadId() == MainThreadId
                       ? ExceptionPolicy.Throw
                       : ThrownInWorkerThread(source, exception);
        }

        /// <summary>
        /// Invoke this method to get an exception police.
        /// </summary>
        /// <param name="source">Object that caught the exception</param>
        /// <param name="exception">Thrown exception</param>
        /// <returns>Policy to take.</returns>
        public static ExceptionPolicy ThrownInWorkerThread(object source, Exception exception)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(exception != null);

            var args = new UnhandledExceptionEventArgs(source, exception) { ActionToTake = ExceptionPolicy.Ignore };
            TriggerUnhandledException(source, args);
            return args.ActionToTake;
        }

        private static void TriggerUnhandledException(object source, UnhandledExceptionEventArgs args)
        {
            UnhandledException(source, args);
        }

        /// <summary>
        /// Catch this event to be able to decide what to do with unhandled exceptions
        /// </summary>
        public static event EventHandler<UnhandledExceptionEventArgs> UnhandledException = delegate { };
    }
}