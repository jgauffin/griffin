using System;
using Griffin.Logging;

namespace Griffin.Core.Logging.Targets
{
    public class ConsoleTarget : ILogTarget
    {
        private readonly ConsoleConfiguration _configuration;

        public ConsoleTarget(ConsoleConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ConsoleTarget()
        {
            _configuration = new ConsoleConfiguration();
        }

        public string Name
        {
            get { return "Console"; }
        }

        protected virtual ConsoleColor GetColor(LogEntry entry)
        {
            switch (entry.LogLevel)
            {
                case LogLevel.Debug:
                    return _configuration.DebugColor;
                case LogLevel.Info:
                    return _configuration.InfoColor;
                case LogLevel.Warning:
                    return _configuration.WarningColor;
                case LogLevel.Error:
                    return _configuration.ErrorColor;
                default:
                    return ConsoleColor.Gray;
            }
        }

        public void Enqueue(LogEntry entry)
        {
            string method = entry.StackFrames[0].GetMethod().ReflectedType.Name + "." + entry.StackFrames[0].GetMethod().Name;

            string tmp = String.Format("{0} {1} {2} {3} {4}",
                                       entry.CreatedAt.ToString("HH:mm:ss.fff"),
                                       entry.ThreadId.ToString().PadLeft(3),
                                       entry.UserName.PadRight(25),
                                       method.PadRight(40),
                                       entry.Message.Replace("\r\n", "\r\n\t"));

            Console.ForegroundColor = GetColor(entry);
            Console.WriteLine(tmp);
            if (entry.Exception != null)
            {
                var exception = entry.Exception;
                string tabs = "\t";
                while (exception != null)
                {
                    Console.WriteLine(tabs + entry.Exception.ToString().Replace("\r\n", "\r\n" + tabs));

                    exception = exception.InnerException;
                    tabs += "\t";
                }
                
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}