using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Griffin.Core.Logging.Filters;
using Griffin.Core.Logging.Targets;
using Griffin.Logging;
using Griffin.Specification.Logging;

namespace Griffin.Core.Logging
{
    public class Logger : ILogger
    {
        private readonly IEnumerable<ILogFilter> _filters;
        private readonly IEnumerable<ILogTarget> _targets;

        public Logger(IEnumerable<ILogFilter> filters, IEnumerable<ILogTarget> targets)
        {
            _filters = filters;
            _targets = targets;
        }

        private void WriteEntry(LogLevel level, string message, Exception exception)
        {
            StackFrame[] frames = new StackTrace(2).GetFrames();
            if (frames != null)
                frames = frames.Take(5).ToArray();

            string userName = Thread.CurrentPrincipal.Identity.Name;
            if (string.IsNullOrEmpty(userName))
                userName = Environment.UserName;

            var entry = new LogEntry
                            {
                                CreatedAt = DateTime.Now,
                                Exception = exception,
                                LogLevel = level,
                                Message = message,
                                StackFrames = frames,
                                ThreadId = Thread.CurrentThread.ManagedThreadId,
                                UserName = userName
                            };

            // find any filter that says no to logging
            if (_filters.Any(filter => !filter.CanLog(entry)))
            {
                Console.WriteLine("Filter blocks");
                return;
            }

            _targets.Each(e => e.Enqueue(entry));
        }

        #region ILogger Members

        public void Debug(string message)
        {
            WriteEntry(LogLevel.Debug, message, null);
        }

        public void Debug(string message, params object[] formatters)
        {
            WriteEntry(LogLevel.Debug, message.FormatWith(formatters), null);
        }

        public void Debug(string message, Exception ex)
        {
            WriteEntry(LogLevel.Debug, message, ex);
        }

        public void Debug(string message, Exception exception, params object[] formatters)
        {
            WriteEntry(LogLevel.Debug, message.FormatWith(formatters), exception);
        }

        public void Error(string message, Exception exception, params object[] formatters)
        {
            WriteEntry(LogLevel.Error, message.FormatWith(formatters), exception);
        }

        public void Info(string message)
        {
            WriteEntry(LogLevel.Info, message, null);
        }

        public void Info(string message, params object[] formatters)
        {
            WriteEntry(LogLevel.Info, message.FormatWith(formatters), null);
        }

        public void Info(string message, Exception ex)
        {
            WriteEntry(LogLevel.Info, message, ex);
        }

        public void Info(string message, Exception exception, params object[] formatters)
        {
            WriteEntry(LogLevel.Info, message.FormatWith(formatters), null);
        }

        public void Warning(string message)
        {
            WriteEntry(LogLevel.Warning, message, null);
        }

        public void Warning(string message, params object[] formatters)
        {
            WriteEntry(LogLevel.Warning, message.FormatWith(formatters), null);
        }

        public void Warning(string message, Exception ex)
        {
            WriteEntry(LogLevel.Warning, message, ex);
        }

        public void Warning(string message, Exception exception, params object[] formatters)
        {
            WriteEntry(LogLevel.Warning, message.FormatWith(formatters), exception);
        }

        public void Error(string message)
        {
            WriteEntry(LogLevel.Error, message, null);
        }

        public void Error(string message, params object[] formatters)
        {
            WriteEntry(LogLevel.Error, message.FormatWith(formatters), null);
        }


        public void Error(string message, Exception ex)
        {
            WriteEntry(LogLevel.Error, message, ex);
        }

        #endregion
    }
}