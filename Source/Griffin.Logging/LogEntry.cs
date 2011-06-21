using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Griffin.Logging
{
    public class LogEntry
    {
        /// <summary>
        /// Gets or sets name of the current identity.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets when entry was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets stack frames
        /// </summary>
        public StackFrame[] StackFrames { get; set; }

        /// <summary>
        /// Gets or sets log message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets exception (optional)
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets id of current thread.
        /// </summary>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets how important the log entry is
        /// </summary>
        public LogLevel LogLevel { get; set; }
    }

}
