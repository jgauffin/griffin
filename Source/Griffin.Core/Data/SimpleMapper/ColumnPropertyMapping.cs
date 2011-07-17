using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Griffin.Core.Data.SimpleMapper
{
    /// <summary>
    ///   Handles a specific property in a class.
    /// </summary>
    /// <typeparam name = "TEntity">The type of the entity/class.</typeparam>
    /// <typeparam name = "TProperty">The type of the property.</typeparam>
    public class ColumnPropertyMapping<TEntity, TProperty> : IColumnPropertyMapping<TEntity>
    {
        private readonly ValueConverter<TEntity> _columnConverter = null;
        private readonly MemberInfo _member;
        private readonly Action<TEntity, TProperty> _setter;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ColumnPropertyMapping{TEntity,TProperty}" /> class.
        /// </summary>
        /// <param name = "columnName">Name of the column in the table.</param>
        /// <param name = "expression">Expression containing property name.</param>
        /// <remarks>
        ///   The column value will automatically be converted to a string if the property is of type <c>string</c>.
        /// </remarks>
        /// <example>
        ///   <code>
        ///     <![CDATA[
        /// var mapping = new PropertyMapping<MyClass, int>("age", entity => entity.Age);
        /// ]]>
        ///   </code>
        /// </example>
        public ColumnPropertyMapping(string columnName, Expression<Func<TEntity, TProperty>> expression)
        {
            if (columnName.IsNullOrEmpty())
                throw new ArgumentException("columnName may not be empty", "columnName");

            _member = expression.GetMember();
            GetterFunc = expression.Compile();
            ColumnName = columnName;

            if (typeof (TProperty) == typeof (string))
                _columnConverter = (converter) => (TProperty) (object) converter.ColumnValue.ToString();

            _setter = expression.CreateSetter();
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ColumnPropertyMapping{TEntity,TProperty}" /> class.
        /// </summary>
        /// <param name = "columnName">Name of the column in the table.</param>
        /// <param name = "expression">The expression containing the property name.</param>
        /// <param name = "columnConverter">The column converter (used to convert the column value type to the property type).</param>
        /// <example>
        ///   <code>
        ///     <![CDATA[
        /// var mapping = new PropertyMapping<MyClass, int>("age", entity => entity.Age, value => int.Parse((string)value));
        /// ]]>
        ///   </code>
        /// </example>
        public ColumnPropertyMapping(string columnName, Expression<Func<TEntity, TProperty>> expression,
                               ValueConverter<TEntity> columnConverter)
            : this(columnName, expression)
        {
            if (columnName.IsNullOrEmpty())
                throw new ArgumentException("columnName may not be empty", "columnName");

            _member = expression.GetMember();
            GetterFunc = expression.Compile();
            ColumnName = columnName;
            _columnConverter = columnConverter;
        }

        /// <summary>
        ///   Gets or sets the expression use to define which property to use
        /// </summary>
        private Func<TEntity, TProperty> GetterFunc { get; set; }

        #region IPropertyMapping<TEntity> Members

        /// <summary>
        ///   Gets or sets the name of the column in the table
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        ///   Gets the name of the property.
        /// </summary>
        public string PropertyName
        {
            get { return _member.Name; }
        }

        /// <summary>
        /// Call converter if it has been specified
        /// </summary>
        /// <param name="context">Current context.</param>
        public object ConvertValue(IValueConverterContext<TEntity> context)
        {
            return _columnConverter != null ? _columnConverter(context) : context.ColumnValue;
        }


        /// <summary>
        ///   Assigns the value to the property.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        /// <param name = "value">Value in the database table.</param>
        /// <remarks>
        ///   Used to convert a value in the database to the type of value used in the class property.
        /// </remarks>
        public virtual void SetValue(TEntity instance, object value)
        {
            if (!(value is TProperty) && _columnConverter == null)
                throw new MappingException(
                    "'{0}' cannot be casted to '{1}' (property '{2}' in '{3}'). You should map using a column converter instead."
                        .FormatWith(
                            value, typeof (TProperty).FullName, PropertyName, _member.ReflectedType.FullName),
                            typeof(TEntity), PropertyName);

            _setter(instance, (TProperty)value);
        }

        /// <summary>
        ///   Get value from a property in the specified instance.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        /// <returns>Property value</returns>
        public object GetValue(TEntity instance)
        {
            return GetterFunc(instance);
        }

        #endregion
    }


}