using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging
{
// ReSharper disable InconsistentNaming
    [ContractClassFor(typeof (ILogger))]
    internal abstract class ILoggerContract : ILogger
// ReSharper restore InconsistentNaming
    {
        #region ILogger Members

        public void Debug(string message)
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Debug(string message, params object[] formatters)
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Debug(string message, Exception exception)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(exception != null);
        }

        public void Debug(string message, Exception exception, params object[] formatters)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(exception != null);
        }

        public void Error(string message)
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Error(string message, params object[] formatters)
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Error(string message, Exception exception)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(exception != null);
        }

        public void Error(string message, Exception exception, params object[] formatters)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(exception != null);
        }

        public void Info(string message)
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Info(string message, params object[] formatters)
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Info(string message, Exception exception)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(exception != null);
        }

        public void Info(string message, Exception exception, params object[] formatters)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(exception != null);
        }

        public void Warning(string message)
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Warning(string message, params object[] formatters)
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Warning(string message, Exception exception)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(exception != null);
        }

        public void Warning(string message, Exception exception, params object[] formatters)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(exception != null);
        }

        #endregion
    }
}