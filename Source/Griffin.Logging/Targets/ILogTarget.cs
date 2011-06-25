namespace Griffin.Logging.Targets
{
    public interface ILogTarget
    {
        string Name { get; }
        void Enqueue(LogEntry entry);
    }
}