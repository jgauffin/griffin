using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Griffin
{
    /// <summary>
    /// Class used to be able to handle uncaught exceptions in worker threads.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Do NOT use this policy handler in your general exception handling. It's intended purpose is
    /// only to decide whether unhandled exceptions should be able to terminate your application
    /// or to keep it running. 
    /// </para>
    /// <para>
    /// Normally you'll just use the policies in Threads and Timers to prevent them from shutting down
    /// the application.</para>
    /// <para>The default policy is to IGNORE all exceptions thrown in other threads than the main thread.</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// public class Program
    /// {
    ///     public static void Main()
    ///     {
    ///         ExceptionPolicyHandler.UnhandledException += OnUnhandledException;
    ///         var myClass = new DemoClass();
    ///         Console.ReadLine();
    ///     }
    /// 
    ///     public void OnUnhandledException(object source, UnhandledExceptionEventArgs args)
    ///     {
    ///         args.ActionToTake = ExceptionPolicy.Throw;
    ///     }
    /// }
    /// 
    /// public class DemoClass
    /// {
    ///     private Thread _worker;
    ///     
    ///     public DemoClass()
    ///     {
    ///         _worker = new Thread(WorkerFunc);
    ///         _worker.Start(null);
    ///     }
    /// 
    ///     public void WorkerFunc(object state)
    ///     {
    ///         try
    ///         {
    ///             var someThingCool = new Coolers();
    ///             someThingCool.DoUnCool();
    ///         }
    ///         catch (Exception err)
    ///         {
    ///              if (ExceptionPolicyHandler.GotUnhandled(this, err) == ExceptionPolicy.Throw)
    ///                  throw;
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
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
        /// <returns>What to do with the exception</returns>
        /// <remarks>
        /// Check the class documentation for a code example.
        /// </remarks>
        /// <seealso cref="ExceptionPolicyHandler"/>
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
        private static ExceptionPolicy ThrownInWorkerThread(object source, Exception exception)
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
        /// <remarks>
        /// Check the class documentation for a code sample.
        /// </remarks>
        /// <seealso cref="ExceptionPolicyHandler"/>
        public static event EventHandler<UnhandledExceptionEventArgs> UnhandledException = delegate { };
    }
}