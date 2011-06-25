using System;

namespace Griffin.Logging
{
    public interface ILogger
    {
        void Debug(string message);
        void Debug(string message, params object[] formatters);
        void Debug(string message, Exception exception);
        void Debug(string message, Exception exception, params object[] formatters);

        void Error(string message);
        void Error(string message, params object[] formatters);
        void Error(string message, Exception exception);
        void Error(string message, Exception exception, params object[] formatters);

        void Info(string message);
        void Info(string message, params object[] formatters);
        void Info(string message, Exception exception);
        void Info(string message, Exception exception, params object[] formatters);

        void Warning(string message);
        void Warning(string message, params object[] formatters);
        void Warning(string message, Exception exception);
        void Warning(string message, Exception exception, params object[] formatters);
    }
}