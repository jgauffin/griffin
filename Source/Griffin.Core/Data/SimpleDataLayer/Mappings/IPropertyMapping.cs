using System.Data;

namespace Griffin.Core.Data.SimpleDataLayer.Mappings
{
    /// <summary>
    ///   Represents a mapping between a column in a table/view and a property in a class.
    /// </summary>
    public interface IPropertyMapping<in TEntity>
    {
        /// <summary>
        ///   Gets the name of the column in the database table.
        /// </summary>
        /// <value>The name of the column.</value>
        string ColumnName { get; }

        /// <summary>
        ///   Gets the name of the property in the class
        /// </summary>
        /// <value>The name of the property.</value>
        string PropertyName { get; }

        /// <summary>
        ///   Get value from a property in the specified instance.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        /// <returns>Value</returns>
        object GetValue(TEntity instance);

        /// <summary>
        ///   Set a value for a property in the specified instance.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        /// <param name = "value">The value.</param>
        void SetValue(TEntity instance, object value);


    }
}