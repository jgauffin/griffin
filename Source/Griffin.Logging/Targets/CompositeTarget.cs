using System.Collections.Generic;
using Griffin.Logging;

namespace Griffin.Core.Logging.Targets
{
    public class CompositeTarget : ILogTarget
    {
        private readonly string _name;
        private List<ILogTarget> _targets = new List<ILogTarget>();

        public CompositeTarget(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public List<ILogTarget> Targets
        {
            get { return _targets; }
        }

        public void Enqueue(LogEntry entry)
        {
            foreach (var target in _targets)
            {
                target.Enqueue(entry);
            }
        }
    }
}
