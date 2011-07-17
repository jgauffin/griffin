using Griffin.Core.Reflection.Mapping;

namespace Griffin.Core.Data.SimpleMapper
{
    /// <summary>
    ///   Represents a mapping between a column in a table/view and a property in a class.
    /// </summary>
    public interface IColumnPropertyMapping<in TEntity> : IPropertyMapping<TEntity>
    {
        /// <summary>
        ///   Gets the name of the column in the database table.
        /// </summary>
        /// <value>The name of the column.</value>
        string ColumnName { get; }

        /// <summary>
        /// Call converter if it has been specified
        /// </summary>
        /// <param name="context">Current context.</param>
        /// <returns>Column value</returns>
        object ConvertValue(IValueConverterContext<TEntity> context);

    }
}