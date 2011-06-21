using Griffin.Logging;

namespace Griffin.Core.Logging.Targets
{
    public interface ILogTarget
    {
        string Name { get; }
        void Enqueue(LogEntry entry);
    }
}
