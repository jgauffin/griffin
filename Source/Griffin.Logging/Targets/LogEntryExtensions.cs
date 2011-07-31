using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Griffin.Logging.Targets
{
    /// <summary>
    /// Extensions for <see cref="LogEntry"/>
    /// </summary>
    public static class LogEntryExtensions
    {
        /// <summary>
        /// Build source using either stackframes or the logged type
        /// </summary>
        /// <param name="entry">Entry containing information</param>
        /// <returns>Formatted source</returns>
        /// <remarks>
        /// Used to switch between <see cref="LogEntry.StackFrames"/> or <see cref="LogEntry.LoggedType"/> depending
        /// on if the stackframes are collected or not.
        /// </remarks>
        public static string StackFrameOrType(this LogEntry entry)
        {
            Contract.Requires<ArgumentNullException>(entry != null);
            Contract.Ensures(Contract.Result<string>() != null);

            if (entry.StackFrames == null)
            {
                return entry.LoggedType == null ? "UnknownSource" : entry.LoggedType.Name;
            }

            
            return string.Format("{0}:{1}", entry.StackFrames[0].GetMethod().ReflectedType.Name,
                                 entry.StackFrames[0].GetMethod().Name);
        }
    }
}
