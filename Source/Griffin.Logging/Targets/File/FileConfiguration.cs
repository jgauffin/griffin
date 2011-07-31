/*
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
using System.Configuration;
using System.Diagnostics.Contracts;

namespace Griffin.Logging.Targets.File
{
    /// <summary>
    /// Configuration used by file targets
    /// </summary>
    /// <remarks>
    /// Check each property for their default values.
    /// </remarks>
    public class FileConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileConfiguration"/> class.
        /// </summary>
        public FileConfiguration()
        {
            DateFormat = "yyyy-MM-dd";
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            DaysToKeep = 30;
            Path = AppDomain.CurrentDomain.BaseDirectory;
            if (string.IsNullOrEmpty(Path))
                throw new InvalidOperationException("AppDomain.CurrentDomain.BaseDirectory is not specified.");
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!string.IsNullOrEmpty(Path), "Path must have been specified.");
            Contract.Invariant(!string.IsNullOrEmpty(DateFormat), "DateFormat must have been specified.");
            Contract.Invariant(!string.IsNullOrEmpty(DateTimeFormat), "DateTimeFormat must have been specified.");
            Contract.Invariant(DaysToKeep >= 0, "Days to keep must be 0 or more.");
        }

        /// <summary>
        /// Gets or sets location to save logs.
        /// </summary>
        /// <value>
        /// Directory name trailed by a path seperator
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets number of days to keep logs.
        /// </summary>
        /// <value>
        /// Default is 30 days
        /// </value>
        /// <remarks>0 = keep for ever.</remarks>
        public int DaysToKeep { get; set; }

        /// <summary>
        /// Gets or sets create a sub folder and place log in it
        /// </summary>
        /// <seealso cref="DateFormat"/>
        public bool CreateDateFolder { get; set; }

        /// <summary>
        /// Gets or sets format to use for dates.
        /// </summary>
        /// <remarks>
        /// Used when creating folder (when <see cref="CreateDateFolder"/> is set to <c>true</c>).
        /// </remarks>
        /// <value>
        /// Default is yyyy-MM-dd
        /// </value>
        public string DateFormat { get; set; }

        /// <summary>
        /// Gets or sets date/time format.
        /// </summary>
        /// <remarks>
        /// Used for each log entry
        /// </remarks>
        /// <value>
        /// Default is yyyy-MM-dd HH:mm:ss.fff
        /// </value>
        public string DateTimeFormat { get; set; }
    }
}