using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Griffin.Core.Data.SimpleMapper
{
    /// <summary>
    ///   Used to map .NET classes against database tables/views.
    /// </summary>
    /// <typeparam name = "TEntity">Type of class (business object) that the mapping is for</typeparam>
    /// <example>
    ///   <code>
    ///     <![CDATA[
    /// [Component]
    /// public class UserMapper<User>
    /// {
    ///     public UserMapper()
    ///     {
    ///         PrimaryKey(user => user.Id, "Id");
    ///         Column(user => user.FirstName, "FIRST_NAME");
    ///         Column(user => user.Age, "AGE", ctx => int.Parse(ctx.Value.ToString()));
    ///     }
    /// }
    /// ]]>
    ///   </code>
    /// </example>
    public class EntityMapper<TEntity> : IEntityMapper<TEntity>
    {
        private readonly List<IColumnPropertyMapping<TEntity>> _mappings = new List<IColumnPropertyMapping<TEntity>>();

        private readonly List<IColumnPropertyMapping<TEntity>> _primaryKeys =
            new List<IColumnPropertyMapping<TEntity>>();

        /// <summary>
        /// Gets full name of the type that the mapper is for.
        /// </summary>
        private string TypeName
        {
            get { return typeof (TEntity).FullName; }
        }

        #region IEntityMapper<TEntity> Members

        /// <summary>
        /// Gets or sets name of the database table (or view).
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Populate an entity using a data record (typically from a <c>IDataReader</c>).
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="record">The record.</param>
        public void Map(TEntity entity, IDataRecord record)
        {
            var ctx = new ValueContext<TEntity> {Entity = entity, TableRow = record};
            foreach (var mapping in _mappings)
            {
                ctx.ColumnValue = record[mapping.ColumnName];
                ctx.ColumnValue = mapping.ConvertValue(ctx);
                mapping.SetValue(entity, ctx.ColumnValue);
            }
        }


        /// <summary>
        ///   Gets the primary key column names
        /// </summary>
        public IEnumerable<string> PrimaryKeys
        {
            get { return _primaryKeys.Select(key => key.ColumnName); }
        }

        /// <summary>
        /// Gets column name from a property name
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Name of the column.
        /// </returns>
        /// <exception cref="MappingException">Property have not been mapped.</exception>
        public string GetColumnName(string propertyName)
        {
            IColumnPropertyMapping<TEntity> mapping = _mappings.FirstOrDefault(m => m.PropertyName == propertyName);
            if (mapping == null)
                throw new MappingException(
                    "Property '{0}' was not mapped for type '{1}'.".FormatWith(propertyName, TypeName),
                    typeof (TEntity), propertyName);

            return mapping.ColumnName;
        }

        /// <summary>
        ///   Gets property name from a column name.
        /// </summary>
        /// <param name = "columnName">Name of the column.</param>
        /// <returns>Name of the property.</returns>
        /// <exception cref = "MappingException">Column have not been mapped.</exception>
        public string GetPropertyName(string columnName)
        {
            IColumnPropertyMapping<TEntity> mapping = _mappings.FirstOrDefault(m => m.ColumnName == columnName);
            if (mapping == null)
                throw new MappingException(
                    "Column '{0}' was not mapped for type '{1}'.".FormatWith(columnName, TypeName),
                    typeof (TEntity), columnName);

            return mapping.PropertyName;
        }

        /// <summary>
        /// Maps the property to a column name.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">Property expression.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <remarks>
        /// See the class documentation for an example.
        /// </remarks>
        public void Column<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName)
        {
            Column(expression, columnName, null);
        }

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
        public void Column<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName,
                                      ValueConverter<TEntity> valueConverter)
        {
            if (_mappings.Exists(prop => prop.ColumnName == columnName))
                throw new InvalidOperationException(
                    "A mapping already exists for column named '{0} in type '{1}'.".FormatWith(columnName,
                                                                                               TypeName));

            var mapping = new ColumnPropertyMapping<TEntity, TProperty>(columnName, expression, valueConverter);
            if (_mappings.Exists(m => m.PropertyName == mapping.PropertyName))
                throw new InvalidOperationException(
                    "A mapping already exists for column named '{0} in type '{1}'.".FormatWith(mapping.PropertyName,
                                                                                               TypeName));

            _mappings.Add(mapping);
        }

        /// <summary>
        ///   Maps the property to a column name.
        /// </summary>
        /// <typeparam name = "TProperty">The type of the property.</typeparam>
        /// <param name = "expression">Property expression.</param>
        /// <param name = "columnName">Name of the column.</param>
        /// <remarks>
        ///   See the class documentation for an example.
        /// </remarks>
        public void PrimaryKey<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName)
        {
            PrimaryKey(expression, columnName, null);
        }

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
        public void PrimaryKey<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName,
                                          ValueConverter<TEntity> valueConverter)
        {
            Column(expression, columnName, valueConverter);
            _primaryKeys.Add(_mappings.First(m => m.ColumnName == columnName));
        }

        #endregion
    }
}