using System;

namespace Griffin.Specification.Logging
{
    internal class NullLogger : ILogger
    {
        #region ILogger Members

        public void Debug(string message)
        {
        }

        public void Debug(string message, params object[] formatters)
        {
        }

        public void Debug(string message, Exception exception)
        {
        }

        public void Debug(string message, Exception exception, params object[] formatters)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, params object[] formatters)
        {
        }

        public void Info(string message, Exception exception)
        {
        }

        public void Info(string message, Exception exception, params object[] formatters)
        {
        }

        public void Warning(string message)
        {
        }

        public void Warning(string message, params object[] formatters)
        {
        }

        public void Warning(string message, Exception exception)
        {
        }

        public void Warning(string message, Exception exception, params object[] formatters)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(string message, params object[] formatters)
        {
        }

        public void Error(string message, Exception exception)
        {
        }

        public void Error(string message, Exception exception, params object[] formatters)
        {
        }

        #endregion
    }
}