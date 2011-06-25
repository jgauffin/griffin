using System;
using System.Collections.Generic;
using System.IO;
using Griffin.Core;
using Griffin.Logging.Filters;
using Griffin.Logging.Targets;
using Griffin.Logging.Targets.File;

namespace Griffin.Logging
{
    public class SimpleLogManager : ILogManager
    {
        private static SimpleLogManager _instance;
        private static readonly List<ILogFilter> _filters = new List<ILogFilter>();
        private static readonly List<ILogTarget> _targets = new List<ILogTarget>();
        private readonly ILogger _logger;

        private SimpleLogManager()
        {
            _logger = new Logger(_filters, _targets);
        }

        #region ILogManager Members

        public ILogger GetLogger(Type type)
        {
            return _logger;
        }

        #endregion

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
    }
}