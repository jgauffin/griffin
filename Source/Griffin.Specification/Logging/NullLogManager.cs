using System;

namespace Griffin.Logging
{
    /// <summary>
    /// Simple wrapper to be able to provide logging objects even if no logging framework have been specified.
    /// </summary>
    internal class NullLogManager : ILogManager
    {
        private static readonly NullLogger _logger = new NullLogger();

        #region ILogManager Members

        public ILogger GetLogger(Type type)
        {
            return _logger;
        }

        #endregion
    }
}