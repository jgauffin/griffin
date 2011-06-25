using System;
using System.Diagnostics;

namespace Griffin.Logging.Targets.File
{
    public class FileTarget : ILogTarget
    {
        private readonly IFileWriter _fileWriter;

        public FileTarget(string name, FileConfiguration configuration)
        {
            _fileWriter = new FileWriter(name, configuration);
        }

        public FileTarget(IFileWriter writer)
        {
            _fileWriter = writer;
        }

        protected FileConfiguration Configuration
        {
            get { return _fileWriter.Configuration; }
        }

        #region ILogTarget Members

        public string Name
        {
            get { return _fileWriter.Name; }
        }

        public void Enqueue(LogEntry entry)
        {
            string entryString = FormatLogEntry(entry);
            _fileWriter.Write(entryString);
        }

        #endregion

        protected virtual string FormatException(Exception exception, int itendentation)
        {
            string text = "\r\n******* EXCEPTION ********\r\n"
                          + exception.ToString().Replace("\r\n", "\r\n".PadRight(itendentation, '\t'));
            if (exception.InnerException != null)
                return text + FormatException(exception.InnerException, itendentation + 1);
            return text;
        }

        protected virtual string FormatLogEntry(LogEntry entry)
        {
            if (entry.Exception != null)
            {
                return string.Format("{0}|{1}|{2}|{3}|{4}|{5}\r\n{6}\r\n",
                                     entry.CreatedAt.ToString(Configuration.DateTimeFormat),
                                     entry.LogLevel,
                                     entry.ThreadId,
                                     FormatUserName(entry.UserName, 40),
                                     FormatStackTrace(entry.StackFrames, 100),
                                     FormatMessage(entry.Message),
                                     FormatException(entry.Exception, 1)
                    );
            }

            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}\r\n",
                                 entry.CreatedAt.ToString(Configuration.DateTimeFormat),
                                 entry.LogLevel,
                                 entry.ThreadId,
                                 FormatUserName(entry.UserName, 40),
                                 FormatStackTrace(entry.StackFrames, 100),
                                 FormatMessage(entry.Message)
                );
        }

        protected virtual string FormatMessage(string message)
        {
            return message.Replace("\r\n", "\r\n\t").Replace('|', ';');
        }

        protected virtual string FormatStackTrace(StackFrame[] frames, int maxSize)
        {
            string typeName = frames[0].GetMethod().ReflectedType.Name;
            string methodName = frames[0].GetMethod().Name;
            string result = string.Format("{0}.{1}", typeName, methodName);
            if (result.Length < maxSize)
                return result;

            result = methodName;
            if (result.Length < maxSize)
                return result;

            return result.Substring(0, maxSize - 1) + ".";
        }

        protected virtual string FormatUserName(string userName, int maxSize)
        {
            if (userName.Length > 0)
            {
                int pos = userName.IndexOf('\\'); //domain name
                if (pos != -1)
                    userName = userName.Substring(pos + 1);
            }

            return userName.Length > maxSize ? userName.Substring(0, maxSize) : userName;
        }

        protected string MaxSize(string tmp, int size)
        {
            if (tmp.Length > size)
                return tmp.Substring(0, size - 1) + ".";
            return tmp;
        }
    }
}