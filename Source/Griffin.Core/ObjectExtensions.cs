using System;

namespace Griffin.Core
{
    /// <summary>
    /// Extensions used to work with objects
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Cast object
        /// </summary>
        /// <typeparam name="T">Type that we are casting to</typeparam>
        /// <param name="instance">Instance being casted.</param>
        /// <returns>Casted object</returns>
        /// <remarks>
        /// No conversions are being made.
        /// </remarks>
        public static T As<T>(this object instance)
        {
            if (typeof(T) == typeof(string))
                return instance == null ? default(T) : (T)(object)instance.ToString();

            return (T) instance;
        }


        /// <summary>
        /// Convert one type to another type
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="instance">Object to convert</param>
        /// <returns>Created object</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///     int myAge = "18".Convert();
        /// ]]>
        /// </code>
        /// </example>
        public static T ConvertTo<T>(this object instance)
        {
            return (T) ConverterService.Convert(instance, typeof (T));
        }

        public static bool Check<T>(this T instance, Func<T, bool> check) where T : class
        {
            if (instance == null)
                return false;

            return check(instance);
        }

        /*
        public static T OrDefault<T>(this T instance, T defaultValue)
        {
            if (instance is string)
            {
                return (string)string.IsNullOrEmpty(instance.As<string>()) ? defaultValue : instance;
            }

            if (instance == default(T) ||  )
        }
         * */
    }
}