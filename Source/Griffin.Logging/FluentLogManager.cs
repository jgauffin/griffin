using System;

namespace Griffin.Logging
{
    internal class FluentLogManager : ILogManager
    {
        public FluentLogManager()
        {
            LogManager.Assign(this);
        }

        #region ILogManager Members

        public ILogger GetLogger(Type type)
        {
            return null;
        }

        #endregion

        public void AddLogger(Logger logger)
        {
        }
    }
}