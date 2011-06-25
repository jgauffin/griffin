using System.Collections.Generic;

namespace Griffin.Logging.Targets
{
    public class CompositeTarget : ILogTarget
    {
        private readonly string _name;
        private readonly List<ILogTarget> _targets = new List<ILogTarget>();

        public CompositeTarget(string name)
        {
            _name = name;
        }

        public List<ILogTarget> Targets
        {
            get { return _targets; }
        }

        #region ILogTarget Members

        public string Name
        {
            get { return _name; }
        }

        public void Enqueue(LogEntry entry)
        {
            foreach (ILogTarget target in _targets)
            {
                target.Enqueue(entry);
            }
        }

        #endregion
    }
}