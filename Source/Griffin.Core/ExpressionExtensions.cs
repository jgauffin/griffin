using System;
using System.Linq.Expressions;

namespace Griffin.Core
{
    /// <summary>
    /// Extension methods for LINQ Expressions
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Get name from a property name from an expression
        /// </summary>
        /// <typeparam name="T">Return value from the property</typeparam>
        /// <param name="instance">Expression</param>
        /// <returns>Property name</returns>
        public static string GetPropertyName<T>(this Expression<Func<T>> instance)
        {
            var body = instance.Body as MemberExpression;
            if (body != null)
            {
                return body.Member.Name;
            }

            var ubody = (UnaryExpression) instance.Body;
            body = (MemberExpression) ubody.Operand;
            return body.Member.Name;
        }
    }
}