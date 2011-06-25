using System.Collections.Generic;
using Griffin.Logging.Targets;

namespace Griffin.Logging
{
    public class FluentTargetConfiguration
    {
        private readonly FluentConfiguration _configuration;
        private readonly List<FluentFilterConfiguration> _filters = new List<FluentFilterConfiguration>();
        private readonly string _name;
        private readonly Dictionary<string, ILogTarget> _targets = new Dictionary<string, ILogTarget>();
        private FluentTargetConfigurationTypes _targetConfig;

        public FluentTargetConfiguration(FluentConfiguration configuration, string name)
        {
            _targetConfig = new FluentTargetConfigurationTypes(this);
            _configuration = configuration;
            _name = name;
        }

        internal string Name
        {
            get { return _name; }
        }

        public IEnumerable<FluentFilterConfiguration> Filters
        {
            get { return _filters; }
        }

        public IEnumerable<ILogTarget> Targets
        {
            get { return _targets.Values; }
        }


        public FluentTargetConfigurationTypes As
        {
            get { return new FluentTargetConfigurationTypes(this); }
        }

        public FluentConfiguration Done
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
}