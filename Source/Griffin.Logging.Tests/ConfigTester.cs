using System.Linq;
using Griffin.Core;
using Griffin.Logging;
using Griffin.Logging.Targets;
using Xunit;


namespace Griffin.Logging.Tests
{
    public class ConfigTester
    {
        [Fact]
        public void TestSimpleLog()
        {
            Configure.Griffin.Logging()
                .LogNamespace("Griffin.Logging.Tests").AndChildNamespaces.ToTargetNamed("Console")
                .AddTarget("Console").As.ConsoleLogger().Done
                .Build();


            var logger = LogManager.GetLogger<ConfigTester>().As<Logger>();
            Assert.IsType(typeof (ConsoleTarget), logger.Targets.First());
        }

        [Fact]
        public void Test()
        {
            Configure.Griffin.Logging()
                .LogNamespace("Griffin.Logging.Tests").AndChildNamespaces.ToTargetNamed("Console")
                .LogNamespace("System").AndChildNamespaces.ToTargetNamed("DefaultFile")
                .LogEverything.ToTargetNamed("DefaultFile")
                .AddTarget("Console")
                    .As.ConsoleLogger().Filter.OnLogLevelBetween(LogLevel.Info, LogLevel.Warning)
                    .Done
                .AddTarget("DefaultFile")
                    .As.FileLogger("ErrorsOnly").Filter.OnLogLevel(LogLevel.Error)
                    .As.FileLogger("Everything")
                    .Done
                .Build();
        }
    }
}

namespace Temp
{
    public class LoggerTest
    {
        private readonly ILogger _logger = LogManager.GetLogger<LoggerTest>();

        public ILogger Logger
        {
            get { return _logger; }
        }
    }
}
