using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging
{
    /// <summary>
    /// Responsible of creating loggers for all types that requests one.
    /// </summary>
    /// <seealso cref="LogManager"/>.
    /// <remarks>
    /// How the logger is configured is up to each implementation to decide.
    /// </remarks>
    [ContractClass(typeof(ILogManagerContract))]
    public interface ILogManager
    {
        /// <summary>
        /// Get a logger for the specified type
        /// </summary>
        /// <param name="type">Type that requests a logger</param>
        /// <returns>A logger (always)</returns>
        /// <remarks>
        /// A logger should <c>always</c> be returned by this method. Simply use a empty
        /// logger if none can be found.
        /// </remarks>
        ILogger GetLogger(Type type);
    }
}