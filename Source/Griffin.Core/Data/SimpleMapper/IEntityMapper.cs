using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Griffin.Core.Data.SimpleMapper
{
    /// <summary>
    /// Defines how Class/Table mappings should be done
    /// </summary>
    /// <typeparam name="TEntity">Class that the mapping is for</typeparam>
    public interface IEntityMapper<TEntity>
    {
        /// <summary>
        /// Populate an entity using a data record (typically from a <c>IDataReader</c>).
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="record">The record.</param>
        void Map(TEntity entity, IDataRecord record);

        /// <summary>
        /// Gets name of the database table.
        /// </summary>
        string TableName { get; }

        /// <summary>
        ///   Gets the primary key column names
        /// </summary>
        IEnumerable<string> PrimaryKeys { get; }

        /// <summary>
        /// Gets column name from a property name
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Name of the column.
        /// </returns>
        /// <exception cref="MappingException">Property have not been mapped.</exception>
        string GetColumnName(string propertyName);

        /// <summary>
        /// Gets property name from a column name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>
        /// Name of the property.
        /// </returns>
        /// <exception cref="MappingException">Column have not been mapped.</exception>
        string GetPropertyName(string columnName);

        /// <summary>
        /// Maps the property to a column name.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">Property expression.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <remarks>
        /// See the class documentation for an example.
        /// </remarks>
        void Column<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName);

        /// <summary>
        /// Maps a property to a column using a value converter.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">The property expression.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="valueConverter">The value converter.</param>
        /// <remarks>
        /// See the class documentation for an example.
        /// </remarks>
        void Column<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName,
                                               ValueConverter<TEntity> valueConverter);

        /// <summary>
        ///   Maps the property to a column name.
        /// </summary>
        /// <typeparam name = "TProperty">The type of the property.</typeparam>
        /// <param name = "expression">Property expression.</param>
        /// <param name = "columnName">Name of the column.</param>
        /// <remarks>
        ///   See the class documentation for an example.
        /// </remarks>
        void PrimaryKey<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName);

        /// <summary>
        ///   Maps a property to a column using a value converter.
        /// </summary>
        /// <typeparam name = "TProperty">The type of the property.</typeparam>
        /// <param name = "expression">The property expression.</param>
        /// <param name = "columnName">Name of the column.</param>
        /// <param name = "valueConverter">The value converter.</param>
        /// <remarks>
        ///   See the class documentation for an example.
        /// </remarks>
        void PrimaryKey<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName,
                                                   ValueConverter<TEntity> valueConverter);
    }
}