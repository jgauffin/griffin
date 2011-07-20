using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Griffin.Core.Data
{
    /// <summary>
    /// Extensions making it easier to work with ADO.NET commands.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Add a parameter to a ADO.NET command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value</param>
        /// <returns>Created parameter</returns>
        public static IDataParameter AddParameter(this IDbCommand command, string name, object value)
        {
            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            command.Parameters.Add(p);
            return p;
        }
    }
}
