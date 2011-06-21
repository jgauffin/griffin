using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Griffin.Core.Logging.Filters;
using Griffin.Core.Logging.Targets;
using Griffin.Core.Logging.Targets.File;
using Griffin.Logging;
using Griffin.Specification;

namespace Griffin.Core.Logging
{
    public class Configuration
    {
        private readonly List<NamespaceLogging> _namespaces = new List<NamespaceLogging>();
        private List<FluentTargetConfiguration> _targets = new List<FluentTargetConfiguration>();
        public static Configuration _generated;
        private FluentLogManager _logManager;


        public Configuration()
        {
            _generated = this;
            AddTarget("fileLogger").As.ConsoleLogger().Filter.OnLogLevelBetween(LogLevel.Debug, LogLevel.Warning).Done
                .LogNamespace("*").AndSubNamespaces.ToTargetNamed("fileLogger");
        }

        /// <summary>
        /// Configure a target that log entries can be written to
        /// </summary>
        /// <param name="name">Name of the target. Must be the same as used by <see cref="LogNamespace"/></param>
        /// <returns>Current configuration instance (to be able to configure fluently)</returns>
        public FluentTargetConfiguration AddTarget(string name)
        {
            return new FluentTargetConfiguration(this, name);
        }

        /// <summary>
        /// Log a specific name space to a named target (see <see cref="AddTarget"/> method)
        /// </summary>
        /// <param name="ns">Name space to log</param>
        /// <returns>Current configuration instance (to be able to configure fluently)</returns>
        public NamespaceLogging LogNamespace(string name)
        {
            /*
			Configure.Griffin.Logging()
				.LogNamespace("NameSpace.Test").AndSubNamespaces.ToTargetNamed("Abc")
				.AddTarget("Abc").As.ConsoleTarget().And.FileLogger("Testing.log", LogLevel.Error, 7).Done
				.LogNamespace("Namespace").AndSubNamespaces.ToTargetsNamed("Errors", "Everything")
				.AddTarget("Everything").As.ConsoleTarget().Done
				.AddTarget("Errors").As.FileLogger("Errors.log", LogLevel.Error, 0).Done;
			
			*/


            var ns = new NamespaceLogging(this, name);
            _namespaces.Add(ns);
            return ns;
        }

        public Configure Build()
        {
            foreach (var ns in _namespaces)
            {
                List<ILogFilter> filters = new List<ILogFilter>();
                filters.Add(new NamespaceFilter(ns.Name, ns.LogSubNamespaces));

                var targets = GetTargets(ns.Targets);

                _logManager.AddLogger(new Logger(filters, targets));
            }
            return Configure.Griffin;
        }

        private IEnumerable<ILogTarget> GetTargets(IEnumerable<string> names)
        {
            List<ILogTarget> targets = new List<ILogTarget>();
            foreach (var targetName in names)
            {
                foreach (var target in _targets)
                {
                    if (target.Name != targetName)
                        continue;

                    foreach (var t in target.Targets)
                        if (!targets.Contains(t))
                            targets.Add(t);
                    break;
                }
            }
            return targets;
        }
    }

    public class FluentTargetConfiguration
    {
        private readonly Configuration _configuration;
        private readonly string _name;
        private readonly List<FluentFilterConfiguration> _filters = new List<FluentFilterConfiguration>();
        private FluentTargetConfigurationTypes _targetConfig;
        private readonly Dictionary<string, ILogTarget> _targets = new Dictionary<string, ILogTarget>();

        public FluentTargetConfiguration(Configuration configuration, string name)
        {
            _targetConfig = new FluentTargetConfigurationTypes(this);
            _configuration = configuration;
            _name = name;
        }

        internal string Name
        { get{ return _name;} }

        public IEnumerable<FluentFilterConfiguration> Filters
        {
            get{ return _filters;}
        }
        public IEnumerable<ILogTarget> Targets
        {
            get { return _targets.Values; }
        }


        public FluentTargetConfigurationTypes As
        {
            get { return new FluentTargetConfigurationTypes(this); }
        }

        public Configuration Done
        {
            get { return _configuration; }
        }

        public FluentFilterConfiguration Filter
        {
            get
            {
                var filter = new FluentFilterConfiguration(this);
                _filters.Add(filter);
                return filter;
            }
        }

        internal void AddInternal(ILogTarget target)
        {
            if (_targets.ContainsKey(target.Name))
                return;

            _targets.Add(target.Name, target);
        }
    }

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

    }

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

    public class NamespaceLogging
    {
        private readonly Configuration _configuration;
        private readonly List<string> _targets = new List<string>();
        private bool _logSubNamespaces;

        public NamespaceLogging(Configuration configuration, string name)
        {
            Name = name;
            _configuration = configuration;
        }

        public NamespaceLogging AndSubNamespaces
        {
            get
            {
                _logSubNamespaces = true;
                return this;
            }
        }

        public Configuration Done
        {
            get { return _configuration; }
        }

        public string Name { get; set; }

        internal bool LogSubNamespaces
        {
            get { return _logSubNamespaces; }
        }

        internal IEnumerable<string> Targets
        {
            get { return _targets; }
        }

        public Configuration ToTargetNamed(string name)
        {
            _targets.Add(name);
            return _configuration;
        }

        public Configuration ToTargetsNamed(params string[] names)
        {
            _targets.AddRange(names);
            return _configuration;
        }
    }
}