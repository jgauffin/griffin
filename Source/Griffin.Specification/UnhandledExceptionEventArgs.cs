using System;

namespace Griffin
{
    /// <summary>
    /// Arguments for the unhandled exception event
    /// </summary>
    /// <remarks>
    /// Set the <see cref="ActionToTake"/> property with the policy that should be used.
    /// </remarks>
    public class UnhandledExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="source">Object that the exception was caught in.</param>
        /// <param name="unhandledException">The unhandled exception.</param>
        public UnhandledExceptionEventArgs(object source, Exception unhandledException)
        {
            Source = source;
            UnhandledException = unhandledException;
        }

        /// <summary>
        /// Gets object that the unhandled exception was caught in.
        /// </summary>
        protected object Source { get; private set; }

        /// <summary>
        /// Gets the exception that was not caught.
        /// </summary>
        public Exception UnhandledException { get; private set; }

        /// <summary>
        /// Gets the action to take for the unhandled exception
        /// </summary>
        public ExceptionPolicy ActionToTake { get; set; }
    }
}