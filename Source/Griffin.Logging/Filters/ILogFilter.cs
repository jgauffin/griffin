namespace Griffin.Logging.Filters
{
    /// <summary>
    /// Used to determine which entries that can be logged
    /// </summary>
    public interface ILogFilter
    {
        /// <summary>
        /// Check a log entry
        /// </summary>
        /// <param name="entry">Entry to validate</param>
        /// <returns>true if entry can be written to a log; otherwise false.</returns>
        bool CanLog(LogEntry entry);
    }
}