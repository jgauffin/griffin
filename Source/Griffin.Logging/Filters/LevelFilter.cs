namespace Griffin.Logging.Filters
{
    /// <summary>
    /// Log entries must be between (or equal to) the specified limits.
    /// </summary>
    public class LevelFilter : ILogFilter
    {
        public LevelFilter(LogLevel minLevel, LogLevel maxLevel)
        {
            MinLevel = minLevel;
            MaxLevel = maxLevel;
        }

        /// <summary>
        /// All log entries must be at least this level
        /// </summary>
        /// <remarks>
        /// Check the actual enum to see which one is the lowest and largest values.
        /// </remarks>
        public LogLevel MinLevel { get; set; }

        /// <summary>
        /// All log entries must be at most this level
        /// </summary>
        /// <remarks>
        /// Check the actual enum to see which one is the lowest and largest values.
        /// </remarks>
        public LogLevel MaxLevel { get; set; }

        #region ILogFilter Members

        /// <summary>
        /// Determines if the specified entry got an allowed log level
        /// </summary>
        /// <param name="entry">Entry to validate against the configured log levels</param>
        /// <returns>true if the entry can be logged; otherwise false.</returns>
        public bool CanLog(LogEntry entry)
        {
            return entry.LogLevel >= MinLevel && entry.LogLevel <= MaxLevel;
        }

        #endregion
    }
}