using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Griffin.Converter;

namespace Griffin.Core.Converters
{
    public class DataRecordMapper<T> : IConverter<IDataRecord, T>
    {
        Dictionary<string, PropertyMapping> _mappings = new Dictionary<string, PropertyMapping>();

        private class PropertyMapping
        {
            /// <summary>
            /// Gets or sets .
            /// </summary>
            public string USerName { get; set; }

            public Action<T> Assigner { get; set; }


        }
        public class DataRecordMappingContext<T, TProperty>
        {
            public T Model { get; private set; }
            public object ColumnValue { get; private set; }
            public IDataReader DataRecord { get; private set; }
            public Func<DataRecordMappingContext<T, TProperty>, TProperty> Converter { get; set; }
            public void SetValue(TProperty value)
            {
                
            }
        }

        public DataRecordMapper()
        {
            var dr = new DataRecordMapper<PropertyMapping>();
            dr.Map(model => model.USerName, "user_name");
            dr.Map(model => model.USerName, "user_name", ctx => ctx.SetValue(ctx.ColumnValue.As<string>()));
        }


        

        public void Map<TProperty>(Expression<Func<T, TProperty>> expression, string columnName)
        {
            //string propertyName
        }

        public void Map<TProperty>(Expression<Func<T, TProperty>> expression, string columnName, Action<DataRecordMappingContext<T, TProperty>> contextAction)
        {
            //string propertyName = expression.GetPropertyName<T, TProperty>();


            //string propertyName
        }

        T IConverter<IDataRecord, T>.Convert(IDataRecord source)
        {
            return default(T);
        }
    }

}
