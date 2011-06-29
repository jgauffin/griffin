using System;

namespace Griffin.Repository.NHibernate
{
    /// <summary>
    /// Something failed in the data layer.
    /// </summary>
    public class DataLayerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataLayerException"/> class.
        /// </summary>
        /// <param name="entityType">Type of entity being fetched.</param>
        /// <param name="errMsg">Error message.</param>
        /// <param name="inner">Inner exception.</param>
        public DataLayerException(Type entityType, string errMsg, Exception inner)
            : base(errMsg, inner)
        {
            EntityType = entityType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataLayerException"/> class.
        /// </summary>
        /// <param name="errMsg">Error message.</param>
        /// <param name="inner">Inner exception.</param>
        public DataLayerException(string errMsg, Exception inner)
            : base(errMsg, inner)
        {
        }

        /// <summary>
        /// Gets or sets type of entity
        /// </summary>
        public Type EntityType { get; set; }

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
                return EntityType != null
                           ? string.Format("Entity({0}): {1}", EntityType.FullName, base.Message)
                           : base.Message;
            }
        }
    }
}