using System;

namespace Griffin.Logging.Filters
{
    /// <summary>
    /// Validates log entries against the namespace that they are written from.
    /// </summary>
    /// <remarks>
    /// Stack frames are used to determine which type is writing to the log. The specified
    /// filter is validated against the namespace that that type exists in.
    /// </remarks>
    public class NamespaceFilter : ILogFilter
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
            _name = name;
            _logSubNamespaces = includeChildNameSpaces;
        }

        #region ILogFilter Members

        public bool CanLog(LogEntry entry)
        {
            Type type = entry.StackFrames[0].GetMethod().ReflectedType;

            if (_logSubNamespaces)
                return type.Namespace.StartsWith(_name);

            return _name == type.Namespace;
        }

        #endregion
    }
}