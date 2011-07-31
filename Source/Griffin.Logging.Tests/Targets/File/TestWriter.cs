using Griffin.Logging.Targets.File;

namespace Griffin.Logging.Tests.Targets.File
{
    public class TestWriter : IFileWriter
    {
        public static readonly TestWriter Instance = new TestWriter();

        private readonly FileConfiguration _configuration = new FileConfiguration();

        public FileConfiguration Configuration
        {
            get { return _configuration; }
        }

        public string Name
        {
            get { return "TestWriter"; }
        }

        public string WrittenEntry { get; private set; }

        public void Write(string logEntry)
        {
            WrittenEntry = logEntry;
        }
    }
}