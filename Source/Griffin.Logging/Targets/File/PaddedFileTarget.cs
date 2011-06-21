using Griffin.Logging;

namespace Griffin.Core.Logging.Targets.File
{
    /// <summary>
    /// File logger using padding for each column.
    /// </summary>
    public class PaddedFileTarget : FileTarget
    {
        public PaddedFileTarget(string name, FileConfiguration configuration) : base(name, configuration)
        {
        }

        protected override string FormatLogEntry(LogEntry entry)
        {
            if (entry.Exception != null)
            {
                return string.Format("{0} {1} {2} {3} {4} {5}\r\n{6}\r\n",
                                     entry.CreatedAt.ToString(Configuration.DateTimeFormat),
                                     entry.LogLevel.ToString().PadRight(8, ' '),
                                     entry.ThreadId.ToString("000"),
                                     FormatUserName(entry.UserName, 16).PadRight(16),
                                     FormatStackTrace(entry.StackFrames, 40).PadRight(40),
                                     FormatMessage(entry.Message),
                                     FormatException(entry.Exception, 1)
                    );
            }

            return string.Format("{0} {1} {2} {3} {4} {5}\r\n",
                                 entry.CreatedAt.ToString(Configuration.DateTimeFormat),
                                 entry.LogLevel.ToString().PadRight(8, ' '),
                                 entry.ThreadId.ToString("000"),
                                 FormatUserName(entry.UserName, 16).PadRight(16),
                                 FormatStackTrace(entry.StackFrames, 40).PadRight(40),
                                 FormatMessage(entry.Message)
                );
        }

    }
}