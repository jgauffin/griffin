using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Griffin.Core.Logging.Filters;
using Griffin.Core.Logging.Targets;
using Griffin.Core.Logging.Targets.File;
using Griffin.Logging;
using Griffin.Specification.Logging;

namespace Griffin.Core.Logging
{
    public class SimpleLogManager : ILogManager
    {
        private ILogger _logger;
        private static SimpleLogManager _instance = null;
        private static List<ILogFilter> _filters = new List<ILogFilter>();
        private static List<ILogTarget> _targets = new List<ILogTarget>();

        private SimpleLogManager()
        {
            _logger = new Logger(_filters, _targets);
        }

        public static void AddFile(string fileNameWithoutExtension, FileConfiguration configuration)
        {
            CreateIfNeeded();
            _targets.Add(new PaddedFileTarget(fileNameWithoutExtension, configuration));
        }

        private static void CreateIfNeeded()
        {
            if (_instance == null)
            {
                _instance = new SimpleLogManager();
                LogManager.Assign(_instance);
            }
        }

        public static void AddFile(string fileName)
        {
            var configuration = new FileConfiguration();
            configuration.Path = Path.IsPathRooted(fileName)
                                     ? Path.GetDirectoryName(fileName)
                                     : AppDomain.CurrentDomain.BaseDirectory;
            configuration.DaysToKeep = 7;
            AddFile(Path.GetFileNameWithoutExtension(fileName), configuration);
        }

        public static void AddConsole(bool createConsole)
        {
            CreateIfNeeded();
            _targets.Add(new ConsoleTarget());
            if (!ConsoleDetector.HasOne && createConsole)
                ConsoleDetector.CreateConsole();
        }

        public static void AddFilter(ILogFilter logFilter)
        {
            
        }


        public ILogger GetLogger(Type type)
        {
            return _logger;
        }
    }
}
