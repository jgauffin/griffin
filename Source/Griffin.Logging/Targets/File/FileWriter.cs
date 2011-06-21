using System;
using System.IO;
using System.Threading;
using System.Globalization;

namespace Griffin.Core.Logging.Targets.File
{
    class FileWriter : IFileWriter
    {
        private readonly string _name;
        string _fileName;

        public FileWriter(string name, FileConfiguration configuration)
        {
            _name = name;
            _configuration = configuration;
            _logDate = DateTime.Today;
        }

        public FileConfiguration Configuration
        {
            get { return _configuration; }
        }


        public string Name
        {
            get { return _name; }
        }


        private string BuildFileName()
        {
            if (_logDate.Date == DateTime.Today && _fileName != null)
                return _fileName;

            string fullPath = Configuration.Path;
            if (!fullPath.EndsWith("\\"))
                fullPath += "\\";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            if (Configuration.CreateDateFolder)
            {
                fullPath += DateTime.Today.ToString(Configuration.DateFormat).Replace('/', '-') + "\\";
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
            }

            fullPath += _name + ".log";
            return fullPath;
        }

        /// <summary>
        /// Is NOT thread safe.
        /// </summary>
        private void CheckOldFolder()
        {
            if (_logDate == DateTime.Today)
                return;

            _logDate = DateTime.Today;
            RemoveOldDirectories();
        }

        public void Write(string logEntry)
        {
            int attempts = 0;
            try
            {
                while (true)
                {
                    try
                    {
                        global::System.IO.File.AppendAllText(BuildFileName(), logEntry);
                        break;
                    }
                    catch (IOException err)
                    {
                        Thread.Sleep(100);
                        ++attempts;
                        if (attempts >= 5)
                        {
                            Console.WriteLine("Failed to write to log {0}: " + err, BuildFileName());
                            if (ExceptionHandler.ThrownInThread(GetType(), err) == ExceptionPolicy.Throw)
                                throw;
                            break;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine("Failed to write to log {0}: " + err, BuildFileName());
                if (ExceptionHandler.ThrownInThread(GetType(), err) == ExceptionPolicy.Throw)
                    throw;

            }
        }

        private void RemoveOldDirectories()
        {
            if (_configuration.DaysToKeep == 0)
                return;

            DateTime minDate = DateTime.Now.AddDays(0 - _configuration.DaysToKeep);
            string[] directories = Directory.GetDirectories(_configuration.Path);
            foreach (string directory in directories)
            {
                int pos = directory.LastIndexOf(Path.DirectorySeparatorChar);
                string name = directory.Substring(pos + 1);

                DateTime folderDate;
                if (
                    !DateTime.TryParseExact(name, _configuration.DateFormat, CultureInfo.InvariantCulture,
                                            DateTimeStyles.None, out folderDate))
                    continue;

                if (folderDate < minDate)
                {
                    Directory.Delete(directory, true);
                }
            }
        }
        private readonly Core.Logging.Targets.File.FileConfiguration _configuration;
        private DateTime _logDate;

    }
}
