using System;

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

        public static void Assign(ILogManager logManager)
        {
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
            return _logManager.GetLogger(typeof (T));
        }
    }
}