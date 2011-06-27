using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging
{
    /// <summary>
    /// Used to write log entries to one or more log targets.
    /// </summary>
    /// <remarks>
    /// It's very important that none of the methods in logger implementations throw exceptions. 
    /// </remarks>
    [ContractClass(typeof(ILoggerContract))]
    public interface ILogger
    {
        /// <summary>
        /// Write a diagnostic message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <remarks>
        /// Log entries which is helpful during debugging. The amount of debug entries can be vast,
        /// and it's therefore not recommended to have them turned on in production systems unless 
        /// it's really required.
        /// </remarks>
        void Debug(string message);

        /// <summary>
        /// Write a diagnostic message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Log entries which is helpful during debugging. The amount of debug entries can be vast,
        /// and it's therefore not recommended to have them turned on in production systems unless 
        /// it's really required.
        /// </remarks>
        void Debug(string message, params object[] formatters);

        /// <summary>
        /// Write a diagnostic message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <remarks>
        /// Log entries which is helpful during debugging. The amount of debug entries can be vast,
        /// and it's therefore not recommended to have them turned on in production systems unless 
        /// it's really required.
        /// </remarks>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Write a diagnostic message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Log entries which is helpful during debugging. The amount of debug entries can be vast,
        /// and it's therefore not recommended to have them turned on in production systems unless 
        /// it's really required.
        /// </remarks>
        void Debug(string message, Exception exception, params object[] formatters);

        /// <summary>
        /// Write an error message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <remarks>
        /// Use this method to log errors in your code that prevent the application from continuing as expected,
        /// but the error is not severe enough to shut down the system.
        /// </remarks>
        void Error(string message);

        /// <summary>
        /// Write an error message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Use this method to log errors in your code that prevent the application from continuing as expected,
        /// but the error is not severe enough to shut down the system.
        /// </remarks>
        void Error(string message, params object[] formatters);

        /// <summary>
        /// Write an error message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <remarks>
        /// Use this method to log errors in your code that prevent the application from continuing as expected,
        /// but the error is not severe enough to shut down the system.
        /// </remarks>
        void Error(string message, Exception exception);

        /// <summary>
        /// Write an error message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Use this method to log errors in your code that prevent the application from continuing as expected,
        /// but the error is not severe enough to shut down the system.
        /// </remarks>
        void Error(string message, Exception exception, params object[] formatters);

        /// <summary>
        /// Write an informational message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <remarks>
        /// Informational messages are more important than Debug messages and are usually state changes like
        /// a user logs on, sends an email etc. There are typically at most one <c>Info</c> message per method.
        /// </remarks>
        void Info(string message);

        /// <summary>
        /// Write an informational message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Informational messages are more important than Debug messages and are usually state changes like
        /// a user logs on, sends an email etc. There are typically at most one <c>Info</c> message per method.
        /// </remarks>
        void Info(string message, params object[] formatters);

        /// <summary>
        /// Write an informational message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <remarks>
        /// Informational messages are more important than Debug messages and are usually state changes like
        /// a user logs on, sends an email etc. There are typically at most one <c>Info</c> message per method.
        /// </remarks>
        void Info(string message, Exception exception);

        /// <summary>
        /// Write an informational message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Informational messages are more important than Debug messages and are usually state changes like
        /// a user logs on, sends an email etc. There are typically at most one <c>Info</c> message per method.
        /// </remarks>
        void Info(string message, Exception exception, params object[] formatters);

        /// <summary>
        /// Write an warning message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <remarks>
        /// Warnings should be written when something did not go as planned, but your application can recover from it
        /// and continue as expected.
        /// </remarks>
        void Warning(string message);

        /// <summary>
        /// Write an warning message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Warnings should be written when something did not go as planned, but your application can recover from it
        /// and continue as expected.
        /// </remarks>
        void Warning(string message, params object[] formatters);

        /// <summary>
        /// Write an warning message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <remarks>
        /// Warnings should be written when something did not go as planned, but your application can recover from it
        /// and continue as expected.
        /// </remarks>
        void Warning(string message, Exception exception);

        /// <summary>
        /// Write an warning message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Warnings should be written when something did not go as planned, but your application can recover from it
        /// and continue as expected.
        /// </remarks>
        void Warning(string message, Exception exception, params object[] formatters);
    }
}