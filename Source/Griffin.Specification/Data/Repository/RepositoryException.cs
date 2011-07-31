using System;
using System.Collections.Generic;
using System.Linq;

namespace Griffin.Data.Repository
{
    /// <summary>
    /// Something failed in the data layer.
    /// </summary>
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="errMsg">Error message.</param>
        /// <param name="inner">Inner exception.</param>
        public RepositoryException(string errMsg, Exception inner)
            : base(errMsg, inner)
        {
        }


        /// <summary>
        /// Gets or sets target as entity/table
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets query / uri.
        /// </summary>
        public string RequestedResource { get; set; }

        /// <summary>
        /// Gets or sets parameters for <see cref="RequestedResource"/>
        /// </summary>
        public IEnumerable<string> Parameters { get; set; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The error message that explains the reason for the exception, or an empty string("").
        /// </returns>
        public override string Message
        {
            get
            {
                if (Target != null)
                    return Target;

                if (Parameters != null )
                    return string.Format(" {0}({1})", RequestedResource, string.Join(", ", Parameters.ToArray()));

                return base.Message;
            }
        }
    }
}