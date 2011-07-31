using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging.Filters
{
    /// <summary>
    /// Validates log entries against the namespace that they are written from.
    /// </summary>
    /// <remarks>
    /// Stack frames are used to determine which type is writing to the log. The specified
    /// filter is validated against the namespace that that type exists in.
    /// </remarks>
    public class NamespaceFilter : IPreFilter
    {
        private readonly bool _logSubNamespaces;
        private readonly string _name;

        /// <summary>
        /// Creates 
        /// </summary>
        /// <param name="name">Namespace that types that log must exist in.</param>
        /// <param name="includeChildNameSpaces">Included all child namespaces</param>
        public NamespaceFilter(string name, bool includeChildNameSpaces)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name));

            _name = name;
            _logSubNamespaces = includeChildNameSpaces;
        }

        /// <summary>
        /// Determines if a log entry can be logged.
        /// </summary>
        /// <param name="loggedType">Type that is logging</param>
        /// <param name="logLevel">Log level</param>
        /// <returns>
        ///   <c>true</c> if the log entry can be logged; otherwise <c>false</c>.
        /// </returns>
        public bool CanLog(Type loggedType, LogLevel logLevel)
        {
            Contract.Assume(_name != null);
            if (_logSubNamespaces)
                return loggedType.Namespace.StartsWith(_name);

            return _name == loggedType.Namespace;
        }
    }
}