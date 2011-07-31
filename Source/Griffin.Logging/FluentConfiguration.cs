﻿/*
 * Copyright (c) 2011, Jonas Gauffin. All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301 USA
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Griffin.Logging
{
    /// <summary>
    /// Fluent configuration api for the logging library.
    /// </summary>
    /// <example>
    /// <code>
    /// Configure.Griffin.Logging()
    ///    .LogNamespace("Griffin.Logging.Tests").AndSubNamespaces.ToTargetNamed("Console")
    ///    .AddTarget("Console").As.ConsoleLogger().Done
    ///    .Build();
    ///</code>
    /// </example>
    public class FluentConfiguration
    {
        public static FluentConfiguration _generated;
        private readonly List<FluentNamespaceLogging> _namespaces = new List<FluentNamespaceLogging>();
        private readonly List<FluentTargetConfiguration> _targets = new List<FluentTargetConfiguration>();


        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConfiguration"/> class.
        /// </summary>
        public FluentConfiguration()
        {
            _generated = this;
        }

        /// <summary>
        /// Logg all namespaces
        /// </summary>
        public FluentNamespaceLogging LogEverything
        {
            get
            {
                var ns = new FluentNamespaceLogging(this, "Everything");
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
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name));

            return new FluentTargetConfiguration(this, name);
        }

        /// <summary>
        /// Log a specific name space to a named target (see <see cref="AddTarget"/> method)
        /// </summary>
        /// <param name="name">Name space to log</param>
        /// <returns>Current configuration instance (to be able to configure fluently)</returns>
        public FluentNamespaceLogging LogNamespace(string name)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name));

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
            var logManager = new FluentLogManager();
            logManager.AddNamespaceFilters(_namespaces);
            Contract.Assume(_targets != null);
            logManager.AddTargets(_targets);
            LogManager.Assign(logManager);
            return Configure.Griffin;
        }
    }
}