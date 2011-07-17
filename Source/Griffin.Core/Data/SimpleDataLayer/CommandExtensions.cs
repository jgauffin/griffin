using System.Collections.Generic;
using System.Data;
using Griffin.Core.Data.SimpleMapper;

namespace Griffin.Core.Data.SimpleDataLayer
{
    /// <summary>
    /// Extensions used to for the data layer.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Execute a command and map the result
        /// </summary>
        /// <typeparam name="T">Type of entity to fill</typeparam>
        /// <param name="command">Command to execute</param>
        /// <returns>Collection of entities</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using (var cmd = connection.CreateCommand())
        /// {
        ///     cmd.CommentText = "WHERE LastName = @name";
        ///     cmd.AddParameter("name", user.LastName);
        ///     var users = cmd.ExecuteAndMapAll<User>();
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static IEnumerable<T> ExecuteAndMapAll<T>(this IDbCommand command) where T : class
        {
            if (!command.CommandText.StartsWith("SELECT"))
            {
                var mapping = MappingProvider.Instance.Get<T>();
                command.CommandText = "SELECT * FROM " + mapping.TableName + " " + command.CommandText;
            }

            using (var reader = command.ExecuteReader())
            {
                LinkedList<T> items = new LinkedList<T>();
                while (reader.Read())
                {
                    var item = ConverterService.Convert<IDataReader, T>(reader);
                    items.AddLast(item);
                }

                return items;
            }
                
        }

        /// <summary>
        /// Execute a command and map the result
        /// </summary>
        /// <typeparam name="T">Type of entity to fill</typeparam>
        /// <param name="command">Command to execute</param>
        /// <returns>Collection of entities</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using (var cmd = connection.CreateCommand())
        /// {
        ///     cmd.CommentText = "WHERE LastName = @name";
        ///     cmd.AddParameter("name", user.LastName);
        ///     var user = cmd.ExecuteMapper<User>();
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static T ExecuteAndMap<T>(this IDbCommand command) where T : class
        {
            if (!command.CommandText.StartsWith("SELECT"))
            {
                var mapping = MappingProvider.Instance.Get<T>();
                command.CommandText = "SELECT * FROM " + mapping.TableName + " " + command.CommandText;
            }

            using (var reader = command.ExecuteReader())
            {
                return !reader.Read() ? null : ConverterService.Convert<IDataReader, T>(reader);
            }
        }

    }
}
