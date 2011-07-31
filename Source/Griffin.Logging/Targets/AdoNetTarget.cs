﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using Griffin.Logging.Filters;

namespace Griffin.Logging.Targets
{
    /// <summary>
    /// Log log entries to a database
    /// </summary>
    /// <remarks>
    /// Was initially created as a demonstration to show how easy it is to add a new target. Instead it got included in
    /// the framework too. You need to create a table in a database to get everything working. Any primary key
    /// should be auto generated and not required in the INSERT statement. Sample table:
    /// <example>
    /// <code lang="sql">
    /// create database LogEntries
    /// (
    ///     id int not null auto_increment primary key,
    ///     UserName varchar(40) not null,
    ///     CreatedAt datetime not null,
    ///     Source varchar(255) not null,
    ///     Message text not null,
    ///     Exception text,
    ///     ThreadId int not null,
    ///     LogLevel int not null
    /// );
    /// </code>
    /// </example>
    /// <para>
    /// Check the <see cref="LogLevel"/> enum to see what kind of values each level has.
    /// </para>
    /// </remarks>
    public class AdoNetTarget : ILogTarget
    {
        private const string InsertStatement =
            @"INSERT INTO LogEntries (UserName, CreatedAt, Source, Message, Exception, ThreadId, LogLevel)" +
            " VALUES(@user, @createdAt, @source, @message, @exception, @threadId, @logLevel)";

        private readonly ConnectionStringSettings _configurationString;
        private readonly List<IPostFilter> _filters = new List<IPostFilter>();
        private readonly DbProviderFactory _providerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoNetTarget"/> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string in app/web.config.</param>
        public AdoNetTarget(string connectionStringName)
        {
            Contract.Requires<ArgumentNullException>(connectionStringName != null);

            _configurationString = ConfigurationManager.ConnectionStrings[connectionStringName];
            _providerFactory = DbProviderFactories.GetFactory(_configurationString.ProviderName);
        }

        private void SaveEntry(LogEntry entry)
        {
            Contract.Requires<ArgumentNullException>(entry != null);

            using (DbConnection connection = _providerFactory.CreateConnection())
            {
                if (connection == null)
                    throw new InvalidOperationException(
                        string.Format("expected to get an connection back from the factory using {0} by provider {1}.",
                                      _configurationString.ConnectionString, _configurationString.ProviderName));

                connection.ConnectionString = _configurationString.ConnectionString;
                connection.Open();

                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = InsertStatement;
                    cmd.AddParameter("userName", entry.UserName);
                    cmd.AddParameter("createdAt", entry.CreatedAt);

                    string value = entry.StackFrameOrType();
                    cmd.AddParameter("source", value);
                    cmd.AddParameter("message", entry.Message);
                    cmd.AddParameter("exception", entry.Exception);
                    cmd.AddParameter("threadId", entry.ThreadId);
                    cmd.AddParameter("logLevel", (int) entry.LogLevel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region ILogTarget Members

        /// <summary>
        /// Gets name of target. 
        /// </summary>
        /// <remarks>
        /// It must be unique for each target. The filename works for file targets etc.
        /// </remarks>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_configurationString.Name))
                    throw new InvalidOperationException("Connection string used for AdoNetTarget must have a name.");
                return  _configurationString.Name;
            }
        }

        /// <summary>
        /// Add a filter for this target.
        /// </summary>
        /// <param name="filter">Filters are used to validate if an entry can be written to a target or not.</param>
        public void AddFilter(IPostFilter filter)
        {
            _filters.Add(filter);
        }

        /// <summary>
        /// Enqueue to be written
        /// </summary>
        /// <param name="entry">Entry being enqueued for logging</param>
        /// <remarks>
        /// The entry might be written directly in the same thread or enqueued to be written
        /// later. It's up to each implementation to decide. Keep in mind that a logger should not
        /// introduce delays in the thread execution. If it's possible that it will delay the thread,
        /// enqueue entries instead and write them in a seperate thread.
        /// </remarks>
        public void Enqueue(LogEntry entry)
        {
            try
            {
                SaveEntry(entry);
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {
#if DEBUG
                throw;
#endif
            }
        }

        #endregion
    }
}