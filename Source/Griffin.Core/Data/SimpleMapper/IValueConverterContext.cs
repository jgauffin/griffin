using System.Data;

namespace Griffin.Core.Data.SimpleMapper
{
    ///<summary>
    /// Defines a context used when converting between property / db column.
    ///</summary>
    ///<typeparam name="TEntity">Entity that the properties is defined in.</typeparam>
    public interface IValueConverterContext<out TEntity>
    {
        /// <summary>
        /// Gets value that is used in the table.
        /// </summary>
        object ColumnValue { get; }

        /// <summary>
        /// Gets the entity that contains the property
        /// </summary>
        TEntity Entity { get; }

        /// <summary>
        /// Gets the complete table row.
        /// </summary>
        IDataRecord TableRow { get; }
    }
}