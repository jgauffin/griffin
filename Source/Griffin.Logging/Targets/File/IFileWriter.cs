namespace Griffin.Core.Logging.Targets.File
{
    public interface IFileWriter
    {
        FileConfiguration Configuration { get; }
        string Name { get; }
        void Write(string logEntry);
    }
}
