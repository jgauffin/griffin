using System;

namespace Griffin.Logging
{
    public interface ILogManager
    {
        ILogger GetLogger(Type type);
    }
}