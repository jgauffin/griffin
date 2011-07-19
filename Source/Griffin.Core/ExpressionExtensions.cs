using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace Griffin.Core
{
    /// <summary>
    /// Extension methods for LINQ Expressions
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        ///   Get member info from an expression
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Member info if conversion was successful; otherwise <c>null</c>.</returns>
        public static MemberInfo GetMember(this LambdaExpression expression)
        {
            MemberExpression memberExp = RemoveUnary(expression.Body);
            return memberExp == null ? null : memberExp.Member;
        }

        /// <summary>
        ///   Get member info from an expression
        /// </summary>
        /// <typeparam name="T">Class type (the class that contains the property)</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///   Member info if conversion was successful; otherwise <c>null</c>.
        /// </returns>
        public static MemberInfo GetMember<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            MemberExpression memberExp = RemoveUnary(expression.Body);
            return memberExp == null ? null : memberExp.Member;
        }

        /// <summary>
        ///   Gets the name of the property.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>Name of property if found; otherwise <c>null</c>.</returns>
        /// <example>
        ///   <code>
        ///     ExpressionHelpers.GetPropertyName(user => user.FirstName);
        ///   </code>
        /// </example>
        public static string GetPropertyName<T, TProp>(Expression<Func<T, TProp>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            return memberExpression == null ? null : memberExpression.Member.Name;
        }

        /// <summary>
        ///   Gets the name of the property.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="instance">Object to get property value for.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///   Name of property if found; otherwise <c>null</c>.
        /// </returns>
        /// <example>
        ///   <code>
        ///     ExpressionHelpers.GetPropertyValue(user =&gt; user.FirstName);
        ///   </code>
        /// </example>
        public static TProp GetPropertyValue<T, TProp>(T instance, Expression<Func<T, TProp>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            return memberExpression == null ? default(TProp) : expression.Compile()(instance);
        }

        /// <summary>
        ///   Remove unary expression from an expression.
        /// </summary>
        /// <param name="toUnwrap">To unwrap.</param>
        /// <returns></returns>
        private static MemberExpression RemoveUnary(Expression toUnwrap)
        {
            if (toUnwrap is UnaryExpression)
            {
                return ((UnaryExpression) toUnwrap).Operand as MemberExpression;
            }

            return toUnwrap as MemberExpression;
        }

        /// <summary>
        /// Create a setter delegate
        /// </summary>
        /// <typeparam name="TEntity">Class type</typeparam>
        /// <typeparam name="TProperty">Property type</typeparam>
        /// <param name="expression">Expression that a delegate should be created for.</param>
        /// <returns>Property setter delegate</returns>
        public static Action<TEntity, TProperty> CreateSetter<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> expression)
        {
            Contract.Requires(expression != null);
            Contract.Ensures(Contract.Result<Action<TEntity, TProperty>>() != null);

            MemberExpression memberExpression;
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpression = (MemberExpression)((UnaryExpression)expression.Body).Operand;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)expression.Body;
                    break;
                default:
                    throw new InvalidOperationException(
                        "Member '{0}' of type '{1}' is not a property.".FormatWith(expression.GetMember().Name,
                                                                     typeof(TEntity).FullName));
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
                throw new InvalidOperationException(
                    "Member '{0}' of type '{1}' is not a property.".FormatWith(expression.GetMember().Name,
                                                                 typeof(TEntity).FullName));
            if (!property.CanWrite)
                throw new InvalidOperationException(
                    "Property '{0}' of type '{1}' is not writable.".FormatWith(expression.GetMember().Name,
                                                                 typeof(TEntity).FullName));

            var setMethod = property.GetSetMethod(true);
            return (Action<TEntity, TProperty>)
                   Delegate.CreateDelegate(typeof(Action<TEntity, TProperty>),
                                           setMethod);
        }
    }
}