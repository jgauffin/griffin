using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging
{
    /// <summary>
    /// Logging framework fascade.
    /// </summary>
    /// <remarks>
    /// Takes care of resolving which loggers a requesting class should get. A <see cref="ILogger"/> implementation
    /// can either log to one targets or multiple ones depending on the configuration.
    /// </remarks>
    public class LogManager
    {
        private static ILogManager _logManager = new NullLogManager();

        /// <summary>
        /// Assigns the specified log manager.
        /// </summary>
        /// <param name="logManager">The log manager.</param>
        /// <remarks>
        /// Assigns a log manager which will be used to generate loggers for
        /// each class that requests one.
        /// </remarks>
        public static void Assign(ILogManager logManager)
        {
            Contract.Requires<ArgumentNullException>(logManager != null);
            _logManager = logManager;
        }

        /// <summary>
        /// Get logger for the specified type
        /// </summary>
        /// <param name="type">Type to get logger for</param>
        /// <returns>A logger implementation.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        public static ILogger GetLogger(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Ensures(Contract.Result<ILogger>() != null);
            return _logManager.GetLogger(type);
        }

        /// <summary>
        /// Get logger for the specified type
        /// </summary>
        /// <returns>A logger implementation.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        public static ILogger GetLogger<T>()
        {
            Contract.Ensures(Contract.Result<ILogger>() != null);
            return _logManager.GetLogger(typeof (T));
        }
    }
}