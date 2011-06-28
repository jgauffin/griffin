using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using Griffin.Converter;

namespace Griffin.Core.Converters
{
    /// <summary>
    /// Takes a name value collection and transforms it to a model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NameValueModelBinder<T> : IConverter<NameValueCollection, T>
    {
        private Dictionary<string, PropertyContext> _converters = new Dictionary<string, PropertyContext>();

        private class PropertyContext
        {
            public T Instance { get; set; }
            public NameValueCollection Collection { get; set; }
            public string PropertyName { get; set; }

        }

        public NameValueModelBinder()
        {
            var type = typeof (T);
            foreach (var property in type.GetProperties())
            {
                if (!property.CanWrite || property.GetIndexParameters().Length != 0)
                    continue;

                if (property.PropertyType == typeof(string))
                {
                    //_converters.Add(property.Name, ctx => value);
                    continue;
                }
                if (IsBasicType(property.PropertyType))
                {
                    PropertyInfo property1 = property;
                    //_converters.Add(property.Name, value => ConverterService.Convert(value, property1.PropertyType));
                    continue;
                }

                // TODO: Break up complex objects.
                //_converters.Add(property.Name, value => ConvertSubModel(name,));
            }
        }


        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <returns>Target type</returns>
        public T Convert(NameValueCollection source)
        {
            throw new NotImplementedException();
        }

        private static bool IsBasicType(Type type)
        {
            return type.IsPrimitive || type == typeof (string);
        }
    }
}
