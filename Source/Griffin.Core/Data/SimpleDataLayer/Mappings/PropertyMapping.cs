using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Griffin.Core.Data.SimpleDataLayer.Mappings
{
    /// <summary>
    ///   Handles a specific property in a class.
    /// </summary>
    /// <typeparam name = "TEntity">The type of the entity/class.</typeparam>
    /// <typeparam name = "TProperty">The type of the property.</typeparam>
    internal class PropertyMapping<TEntity, TProperty> : IPropertyMapperWithConverter<TEntity>
    {
        private readonly ValueConverter<TEntity> _columnConverter = ctx => ctx.ColumnValue;
        private readonly MemberInfo _member;
        private readonly Action<TEntity, TProperty> _setter;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PropertyMapping&lt;TEntity, TProperty&gt;" /> class.
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
        public PropertyMapping(string columnName, Expression<Func<TEntity, TProperty>> expression)
        {
            if (columnName.IsNullOrEmpty())
                throw new ArgumentException("columnName may not be empty", "columnName");

            _member = expression.GetMember();
            PropertyNameExpression = expression.Compile();
            ColumnName = columnName;

            if (typeof (TProperty) == typeof (string))
                _columnConverter = (converter) => (TProperty) (object) converter.ColumnValue.ToString();

            _setter = CreateSetter(expression);
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PropertyMapping&lt;TEntity, TProperty&gt;" /> class.
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
        public PropertyMapping(string columnName, Expression<Func<TEntity, TProperty>> expression,
                               ValueConverter<TEntity> columnConverter)
            : this(columnName, expression)
        {
            if (columnName.IsNullOrEmpty())
                throw new ArgumentException("columnName may not be empty", "columnName");

            _member = expression.GetMember();
            PropertyNameExpression = expression.Compile();
            ColumnName = columnName;
            _columnConverter = columnConverter;
        }

        /// <summary>
        ///   Gets or sets the expression use to define which property to use
        /// </summary>
        public Func<TEntity, TProperty> PropertyNameExpression { get; private set; }

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
        /// Converts the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public object ConvertValue(IValueConverterContext<TEntity> context)
        {
            return _columnConverter(context);
        }

        /// <summary>
        ///   Assigns the value to the property.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        /// <param name = "value">Value in the database table.</param>
        /// <remarks>
        ///   Used to convert a value in the database to the type of value used in the class property.
        /// </remarks>
        public void SetValue(TEntity instance, object value)
        {
            if (!(value is TProperty) && _columnConverter == null)
                throw new InvalidCastException(value + " cannot be casted to " + typeof(TProperty).FullName +
                                               " (property " + PropertyName + " in " + _member.ReflectedType.FullName +
                                               "). You should map using a column converter instead.");
            _setter(instance, (TProperty)value);
        }

        /// <summary>
        ///   Get value from a property in the specified instance.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        /// <returns>Property value</returns>
        public object GetValue(TEntity instance)
        {
            return PropertyNameExpression(instance);
        }

        #endregion

        /// <summary>
        ///   Builds a delegate used to assign values to the property.
        /// </summary>
        /// <param name = "expression"></param>
        /// <returns></returns>
        private Action<TEntity, TProperty> CreateSetter(Expression<Func<TEntity, TProperty>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("getter");

            MemberExpression memberExpression;
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpression = (MemberExpression) ((UnaryExpression) expression.Body).Operand;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression) expression.Body;
                    break;
                default:
                    throw new MappingException("Member is not a property", typeof (TEntity), _member.Name);
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
                throw new MappingException("Member is not a property.", typeof (TEntity), _member.Name);
            if (!property.CanWrite)
                throw new MappingException("Property is not writable.", typeof (TEntity), _member.Name);

            var setMethod = property.GetSetMethod(true);
            return (Action<TEntity, TProperty>)
                   Delegate.CreateDelegate(typeof (Action<TEntity, TProperty>),
                                           setMethod);
        }


             
    }

    public interface IValueConverterContext<TEntity>
    {
        object ColumnValue { get; }
        TEntity Entity { get; }
        IDataRecord TableRow { get; }
    }

    public class ValueContext<TEntity> : IValueConverterContext<TEntity>
    {
        public object ColumnValue { get; set; }
        public TEntity Entity { get; set; }
        public IDataRecord TableRow { get; set; }
    }

    public delegate object ValueConverter<TEntity>(IValueConverterContext<TEntity> context);  
}