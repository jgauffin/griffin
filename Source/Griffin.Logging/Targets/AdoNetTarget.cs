using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Griffin.Logging.Filters;

namespace Griffin.Logging.Targets
{
    class AdoNetTarget : ILogTarget
    {
        private DbProviderFactory _providerFactory;
        private ConnectionStringSettings _configurationString;
        private List<ILogFilter> _filters = new List<ILogFilter>();

        private const string InsertStatement =
            @"INSERT INTO LogEntries (UserName, CreatedAt, Source, Message, Exception, ThreadId, LogLevel)" +
            "(@user, @createdAt, @source, @message, @exception, @threadId, @logLevel)";

        public AdoNetTarget(string connectionStringName)
        {
            _configurationString = ConfigurationManager.ConnectionStrings[connectionStringName];
            _providerFactory = DbProviderFactories.GetFactory(_configurationString.ProviderName);
        }

        /// <summary>
        /// Gets name of target. 
        /// </summary>
        /// <remarks>
        /// It must be unique for each target. The filename works for file targets etc.
        /// </remarks>
        public string Name
        {
            get { return _configurationString.Name; }
        }

        /// <summary>
        /// Add a filter for this target.
        /// </summary>
        /// <param name="filter">Filters are used to validate if an entry can be written to a target or not.</param>
        public void AddFilter(ILogFilter filter)
        {
            _filters.Add(filter);
        }

        /// <summary>
        /// Enqueue to be written
        /// </summary>
        /// <param name="entry"></param>
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
            catch
            { }
        }

        private void SaveEntry(LogEntry entry)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _configurationString.ConnectionString;
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = InsertStatement;
                    cmd.AddParameter("userName", entry.UserName);
                    cmd.AddParameter("createdAt", entry.CreatedAt);

                    var value = entry.StackFrames.First().GetMethod().ReflectedType.Name + "."
                              + entry.StackFrames.First().GetMethod().Name;
                    cmd.AddParameter("source", value);
                    cmd.AddParameter("message", entry.Message);
                    cmd.AddParameter("exception", entry.Exception);
                    cmd.AddParameter("threadId", entry.ThreadId);
                    cmd.AddParameter("logLevel", entry.LogLevel.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public static class IDbCommandExtensions
    {
        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            command.Parameters.Add(p);
        }
    }
}
