using System.Collections.Generic;
using Griffin.Logging.Filters;

namespace Griffin.Logging
{
    public class FluentFilterConfiguration
    {
        private readonly FluentTargetConfiguration _configuration;
        private readonly List<ILogFilter> _filters = new List<ILogFilter>();

        public FluentFilterConfiguration(FluentTargetConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<ILogFilter> Filters
        {
            get { return _filters; }
        }

        public FluentTargetConfiguration Done
        {
            get { return _configuration; }
        }

        public FluentTargetConfiguration OnLogLevelBetween(LogLevel minimum, LogLevel maximum)
        {
            _filters.Add(new LevelFilter(minimum, maximum));
            return _configuration;
        }

        public FluentTargetConfiguration OnLogLevel(LogLevel error)
        {
            _filters.Add(new LevelFilter(error, error));
            return _configuration;
        }
    }
}