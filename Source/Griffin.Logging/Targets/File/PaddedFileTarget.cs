/*
 * Copyright (c) 2011, Jonas Gauffin. All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301 USA
 */

using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging.Targets.File
{
    /// <summary>
    /// File logger using padding for each column.
    /// </summary>
    /// <remarks>
    /// Write a column based output to a file. Each column value is automatically truncated to fit
    /// in a column
    /// </remarks>
    public class PaddedFileTarget : FileTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaddedFileTarget"/> class.
        /// </summary>
        /// <param name="name">Unique name for this target. Could be the file name</param>
        /// <param name="configuration">The configuration.</param>
        public PaddedFileTarget(string name, FileConfiguration configuration) : base(name, configuration)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(name));
            Contract.Requires<ArgumentNullException>(configuration != null);
        }

                /// <summary>
        /// Initializes a new instance of the <see cref="FileTarget"/> class.
        /// </summary>
        /// <param name="writer">Writer being used to to write log entries.</param>
        public PaddedFileTarget(IFileWriter writer)
            :base(writer)
        {
                    Contract.Requires<ArgumentNullException>(writer != null);
        }

        /// <summary>
        /// Format a log entry as it should be written to the file
        /// </summary>
        /// <param name="entry">Entry to format</param>
        /// <returns>
        /// Formatted entry
        /// </returns>
        protected override string FormatLogEntry(LogEntry entry)
        {
            string result;
            var ex = entry.Exception; // to satisfy code contracts
            if (ex != null)
            {
                result= string.Format("{0} {1} {2} {3} {4} {5}\r\n{6}\r\n",
                                     entry.CreatedAt.ToString(Configuration.DateTimeFormat),
                                     entry.LogLevel.ToString().PadRight(8, ' '),
                                     entry.ThreadId.ToString("000"),
                                     FormatUserName(entry.UserName, 16).PadRight(16),
                                     FormatStackTrace(entry.StackFrames, 40).PadRight(40),
                                     FormatMessage(entry.Message),
                                     FormatException(ex, 1)
                    );
            }
            else
            {
                result= string.Format("{0} {1} {2} {3} {4} {5}\r\n",
                              entry.CreatedAt.ToString(Configuration.DateTimeFormat),
                              entry.LogLevel.ToString().PadRight(8, ' '),
                              entry.ThreadId.ToString("000"),
                              FormatUserName(entry.UserName, 16).PadRight(16),
                              FormatStackTrace(entry.StackFrames, 40).PadRight(40),
                              FormatMessage(entry.Message)
                    );
            }

            // do make Code Contracts shut up.
            return string.IsNullOrEmpty(result) ? "Invalid entry" : result;
        }
    }
}