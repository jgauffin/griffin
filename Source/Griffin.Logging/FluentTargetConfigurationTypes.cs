using System.IO;
using System.Reflection;
using Griffin.Logging.Targets;
using Griffin.Logging.Targets.File;

namespace Griffin.Logging
{
    public class FluentTargetConfigurationTypes
    {
        private readonly FluentTargetConfiguration _configuration;

        public FluentTargetConfigurationTypes(FluentTargetConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Add(ILogTarget target)
        {
            _configuration.AddInternal(target);
        }

        public FluentTargetConfiguration ConsoleLogger()
        {
            Add(new ConsoleTarget(new ConsoleConfiguration()));
            return _configuration;
        }

        public FluentTargetConfiguration ConsoleLogger(ConsoleConfiguration config)
        {
            Add(new ConsoleTarget(config));

            return _configuration;
        }


        public FluentTargetConfiguration FileLogger(string name, FileConfiguration config)
        {
            Add(new FileTarget(name, config));
            return _configuration;
        }

        public FluentTargetConfiguration FileLogger(string name)
        {
            var config = new FileConfiguration();
            config.Path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + "\\logs\\";
            config.DaysToKeep = 7;
            Add(new FileTarget(name, config));
            return _configuration;
        }

        public FluentTargetConfiguration PaddedFileLogger(string name, FileConfiguration config)
        {
            Add(new PaddedFileTarget(name, config));
            return _configuration;
        }

        public FluentTargetConfiguration PaddedFileLogger(string name)
        {
            var config = new FileConfiguration();
            config.Path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + "\\logs\\";
            config.DaysToKeep = 7;
            Add(new PaddedFileTarget(name, config));
            return _configuration;
        }
    }
}