using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Griffin.Core
{
    public static class ReflectionHelpers
    {
        public static T GetAttribute<T>(this Type type, bool inherit) where T :Attribute
        {
            return type.GetCustomAttributes(typeof (T), inherit).FirstOrDefault().As<T>();
        }

        public static IEnumerable<T> GetAttributes<T>(this Type type, bool inherit) where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), inherit).Cast<T>();
        }

        public static T GetAttribute<T>(this Assembly assembly, bool inherit) where T : Attribute
        {
            return assembly.GetCustomAttributes(typeof(T), inherit).FirstOrDefault().As<T>();
        }

    }
}
