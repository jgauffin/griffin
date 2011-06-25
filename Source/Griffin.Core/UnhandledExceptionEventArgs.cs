using System;

namespace Griffin.Core
{
    /// <summary>
    /// Arguments for the unhandled exception event
    /// </summary>
    public class UnhandledExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="unhandledException">The unhandled exception.</param>
        public UnhandledExceptionEventArgs(string message, Exception unhandledException)
        {
            Message = message;
            UnhandledException = unhandledException;
        }

        /// <summary>
        /// Gets message with information about the exception
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets exception that was not caught.
        /// </summary>
        public Exception UnhandledException { get; private set; }
    }
}