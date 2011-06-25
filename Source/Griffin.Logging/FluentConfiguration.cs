﻿using System.Collections.Generic;
using Griffin.Logging.Filters;
using Griffin.Logging.Targets;

namespace Griffin.Logging
{
    public class FluentConfiguration
    {
        public static FluentConfiguration _generated;
        private readonly List<FluentNamespaceLogging> _namespaces = new List<FluentNamespaceLogging>();
        private readonly List<FluentTargetConfiguration> _targets = new List<FluentTargetConfiguration>();
        private FluentLogManager _logManager;


        public FluentConfiguration()
        {
            _generated = this;
            AddTarget("fileLogger").As.ConsoleLogger().Filter.OnLogLevelBetween(LogLevel.Debug, LogLevel.Warning).Done
                .LogNamespace("*").AndSubNamespaces.ToTargetNamed("fileLogger");
        }

        /// <summary>
        /// Logg all namespaces
        /// </summary>
        public FluentNamespaceLogging LogEverything
        {
            get
            {
                var ns = new FluentNamespaceLogging(this, null);
                _namespaces.Add(ns);
                return ns;
            }
        }

        /// <summary>
        /// Configure a target that log entries can be written to
        /// </summary>
        /// <param name="name">Name of the target. Must be the same as used by <see cref="LogNamespace"/></param>
        /// <returns>Current configuration instance (to be able to configure fluently)</returns>
        public FluentTargetConfiguration AddTarget(string name)
        {
            return new FluentTargetConfiguration(this, name);
        }

        /// <summary>
        /// Log a specific name space to a named target (see <see cref="AddTarget"/> method)
        /// </summary>
        /// <param name="ns">Name space to log</param>
        /// <returns>Current configuration instance (to be able to configure fluently)</returns>
        public FluentNamespaceLogging LogNamespace(string name)
        {
            var ns = new FluentNamespaceLogging(this, name);
            _namespaces.Add(ns);
            return ns;
        }

        /// <summary>
        /// Build the logging configuration and assign a log manager.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Call this method to generate the configuration. It will also assign a LogManager which means that
        /// everything is set to start using the logging system.
        /// </remarks>
        public Configure Build()
        {
            foreach (FluentNamespaceLogging ns in _namespaces)
            {
                var filters = new List<ILogFilter>();
                filters.Add(new NamespaceFilter(ns.Name, ns.LogSubNamespaces));

                IEnumerable<ILogTarget> targets = GetTargets(ns.Targets);

                _logManager.AddLogger(new Logger(filters, targets));
            }

            LogManager.Assign(_logManager);
            return Configure.Griffin;
        }

        private IEnumerable<ILogTarget> GetTargets(IEnumerable<string> names)
        {
            var targets = new List<ILogTarget>();
            foreach (string targetName in names)
            {
                foreach (FluentTargetConfiguration target in _targets)
                {
                    if (target.Name != targetName)
                        continue;

                    foreach (ILogTarget t in target.Targets)
                        if (!targets.Contains(t))
                            targets.Add(t);
                    break;
                }
            }
            return targets;
        }
    }

    public static class ConfigureExtension
    {
        public static FluentConfiguration Logging(this Configure instance)
        {
            return new FluentConfiguration();
        }
    }
}