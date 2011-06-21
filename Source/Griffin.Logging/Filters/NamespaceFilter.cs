using Griffin.Logging;

namespace Griffin.Core.Logging.Filters
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
        private readonly string _name;
        private readonly bool _logSubNamespaces;

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

        public bool CanLog(LogEntry entry)
        {
            var type = entry.StackFrames[0].GetMethod().ReflectedType;

            if (_logSubNamespaces)
                return type.Namespace.StartsWith(_name);

            return _name == type.Namespace;
        }
    }
}
