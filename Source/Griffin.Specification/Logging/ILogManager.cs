using System;

namespace Griffin.Specification.Logging
{
    public interface ILogManager
    {
        ILogger GetLogger(Type type);
    }
}