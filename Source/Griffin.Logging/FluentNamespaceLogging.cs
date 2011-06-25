using System.Collections.Generic;

namespace Griffin.Logging
{
    public class FluentNamespaceLogging
    {
        private readonly FluentConfiguration _configuration;
        private readonly List<string> _targets = new List<string>();
        private bool _logSubNamespaces;

        public FluentNamespaceLogging(FluentConfiguration configuration, string name)
        {
            Name = name;
            _configuration = configuration;
        }

        public FluentNamespaceLogging AndSubNamespaces
        {
            get
            {
                _logSubNamespaces = true;
                return this;
            }
        }

        public FluentConfiguration Done
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

        public FluentConfiguration ToTargetNamed(string name)
        {
            _targets.Add(name);
            return _configuration;
        }

        public FluentConfiguration ToTargetsNamed(params string[] names)
        {
            _targets.AddRange(names);
            return _configuration;
        }
    }
}