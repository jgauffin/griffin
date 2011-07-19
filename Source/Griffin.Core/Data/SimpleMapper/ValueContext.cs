using System.Data;

namespace Griffin.Core.Data.SimpleMapper
{
    /// <summary>
    /// Context used when manually converting a column type to a property type.
    /// </summary>
    /// <typeparam name="TEntity">Entity model type</typeparam>
    public class ValueContext<TEntity> : IValueConverterContext<TEntity>
    {
        /// <summary>
        /// Gets or sets value in the column value
        /// </summary>
        public object ColumnValue { get; set; }

        /// <summary>
        /// Gets or sets entity that the property exists in
        /// </summary>
        public TEntity Entity { get; set; }

        /// <summary>
        /// Gets or sets the entire table row
        /// </summary>
        public IDataRecord TableRow { get; set; }
    }
}