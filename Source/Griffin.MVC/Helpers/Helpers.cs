using System;
using System.Linq.Expressions;

namespace Griffin.MVC.Helpers
{
    public static class Helpers
    {
        public static string GetInputName<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Call)
            {
                var methodCallExpression = (MethodCallExpression) expression.Body;
                string name = GetInputName(methodCallExpression);
                return name.Substring(expression.Parameters[0].Name.Length + 1);
            }
            return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1);
        }

        private static string GetInputName(MethodCallExpression expression)
        {
            // p => p.Foo.Bar().Baz.ToString() => p.Foo OR throw...
            var methodCallExpression = expression.Object as MethodCallExpression;
            return methodCallExpression != null ? GetInputName(methodCallExpression) : expression.Object.ToString();
        }
    }
}