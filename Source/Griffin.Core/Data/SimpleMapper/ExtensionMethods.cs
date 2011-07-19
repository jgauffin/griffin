using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Data.SimpleMapper
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static DateTime ToDateTime<TEntity>(this IValueConverterContext<TEntity> context)
        {
            return context.ColumnValue is DateTime
                       ? context.ColumnValue.As<DateTime>()
                       : DateTime.Parse(context.ColumnValue.ToString());
        }
    }
}
