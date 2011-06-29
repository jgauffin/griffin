using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Griffin.Core.Data.SimpleDataLayer.Mappings
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
    ///         Column(user => user.Age, "AGE", value => int.Parse(value.ToString()));
    ///     }
    /// }
    /// ]]>
    ///   </code>
    /// </example>
    public class EntityMapper<TEntity> : IEntityMapper<TEntity>
    {
        private readonly List<IPropertyMapperWithConverter<TEntity>> _mappings = new List<IPropertyMapperWithConverter<TEntity>>();
        private readonly List<IPropertyMapperWithConverter<TEntity>> _primaryKeys = new List<IPropertyMapperWithConverter<TEntity>>();

        private string TypeName
        {
            get { return typeof (TEntity).FullName; }
        }

        #region IEntityMapper<TEntity> Members

        /// <summary>
        ///   Populate an entity using a data record (typically from a <c>IDataReader</c>).
        /// </summary>
        /// <param name = "entity">The entity.</param>
        /// <param name = "record">The record.</param>
        public void PopulateEntity(TEntity entity, IDataRecord record)
        {
            var ctx = new ValueContext<TEntity> {Entity = entity, TableRow = record};
            foreach (var mapping in _mappings)
            {
                ctx.ColumnValue = record[mapping.ColumnName];
                var convertedValue = mapping.ConvertValue(ctx);
                mapping.SetValue(entity, convertedValue);
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
        ///   Gets column name from a property name
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
        /// <returns>Name of the column.</returns>
        /// <exception cref = "MappingException">Property have not been mapped.</exception>
        public string GetColumnName(string propertyName)
        {
            var mapping = _mappings.FirstOrDefault(m => m.PropertyName == propertyName);
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
            var mapping = _mappings.FirstOrDefault(m => m.ColumnName == columnName);
            if (mapping == null)
                throw new MappingException(
                    "Column '{0}' was not mapped for type '{1}'.".FormatWith(columnName, TypeName),
                    typeof (TEntity), columnName);

            return mapping.PropertyName;
        }

        #endregion

        /// <summary>
        ///   Maps the property to a column name.
        /// </summary>
        /// <typeparam name = "TProperty">The type of the property.</typeparam>
        /// <param name = "expression">Property expression.</param>
        /// <param name = "columnName">Name of the column.</param>
        /// <remarks>
        ///   See the class documentation for an example.
        /// </remarks>
        public void Column<TProperty>(Expression<Func<TEntity, TProperty>> expression, string columnName)
        {
            if (_mappings.Exists(prop => prop.ColumnName == columnName))
                throw new InvalidOperationException("A mapping already exists for column named '" + columnName +
                                                    "' in type '" + TypeName + "'.");
            var mapping = new PropertyMapping<TEntity, TProperty>(columnName, expression);
            if (_mappings.Exists(m => m.PropertyName == mapping.PropertyName))
                throw new InvalidOperationException("A mapping already exists for a proeprty named '" +
                                                    mapping.PropertyName +
                                                    "' in type '" + TypeName + "'.");

            _mappings.Add(mapping);
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
                throw new InvalidOperationException("A mapping already exists for column named '" + columnName +
                                                    "' in type '" + TypeName + "'.");

            var mapping = new PropertyMapping<TEntity, TProperty>(columnName, expression, valueConverter);
            if (_mappings.Exists(m => m.PropertyName == mapping.PropertyName))
                throw new InvalidOperationException("A mapping already exists for a proeprty named '" +
                                                    mapping.PropertyName +
                                                    "' in type '" + TypeName + "'.");

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
            if (_primaryKeys.Exists(prop => prop.ColumnName == columnName))
                throw new InvalidOperationException("A mapping already exists for primary key named '" + columnName +
                                                    "' in type '" + TypeName + "'.");
            var mapping = new PropertyMapping<TEntity, TProperty>(columnName, expression);
            if (_primaryKeys.Exists(m => m.PropertyName == mapping.PropertyName))
                throw new InvalidOperationException("A mapping already exists for a property named '" +
                                                    mapping.PropertyName +
                                                    "' in type '" + TypeName + "'.");

            Column(expression, columnName);
            _primaryKeys.Add(mapping);
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
            if (_mappings.Exists(prop => prop.ColumnName == columnName))
                throw new InvalidOperationException("A mapping already exists for column named '" + columnName +
                                                    "' in type '" + TypeName + "'.");

            var mapping = new PropertyMapping<TEntity, TProperty>(columnName, expression, valueConverter);
            if (_mappings.Exists(m => m.PropertyName == mapping.PropertyName))
                throw new InvalidOperationException("A mapping already exists for a proeprty named '" +
                                                    mapping.PropertyName +
                                                    "' in type '" + TypeName + "'.");

            Column(expression, columnName, valueConverter);
            _primaryKeys.Add(mapping);
        }
    }
    /*

    public interface IValueConverterContext<TEntity>
    {
        object ColumnValue { get; }
        TEntity Entity { get; }
        IDataRecord TableRow { get; }
    }

    public class ValueContext<TEntity, TProperty> : IValueConverterContext<TEntity>
    {
        public object ColumnValue { get; private set; }
        public TEntity Entity { get; private set; }
        public IDataRecord TableRow { get; private set; }
    }

    public delegate TProperty ValueConverter<TEntity, TProperty>(ValueContext<TEntity, TProperty> context);   
     * */
}