using System;
using Griffin.Logging;
using Griffin.Specification.Logging;

namespace Griffin.Core.Logging
{
    internal class FluentLogManager : ILogManager
    {
        public FluentLogManager()
        {
            LogManager.Assign(this);            
        }

        public ILogger GetLogger(Type type)
        {
            return null;
        }

        public void AddLogger(Logger logger)
        {
            
        }
    }
}
