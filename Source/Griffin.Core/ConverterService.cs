using System;
using Griffin.Converter;
using Griffin.Core.Converters;

namespace Griffin.Core
{
    /// <summary>
    /// Used to do conversion between types.
    /// </summary>
    /// <remarks>
    /// Will use the <see cref="DefaultConverterService"/> per default. Assign your own implementation if required.
    /// </remarks>
    public static class ConverterService
    {
        private static IConverterService _converter = new DefaultConverterService();

        /// <summary>
        /// Assigns the specified converter service.
        /// </summary>
        /// <param name="converterService">The converter service.</param>
        public static void Assign(IConverterService converterService)
        {
            _converter = converterService;
        }

        /// <summary>
        /// Convert from a type to another.
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="from">Object to convert</param>
        /// <returns>Created object</returns>
        public static TTo Convert<TFrom, TTo>(TFrom from)
        {
            return _converter.Convert<TFrom, TTo>(from);
        }

        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="targetType">Type to convert to</param>
        /// <returns>Created object</returns>
        public static object Convert(object source, Type targetType)
        {
            return _converter.Convert(source, targetType);
        }

        /// <summary>
        /// Register a new converter
        /// </summary>
        /// <typeparam name="TFrom">Type being converted</typeparam>
        /// <typeparam name="TTo">Type being created</typeparam>
        /// <param name="converter">Actual converter</param>
        /// <remarks>
        /// Any existing converter will be replaced with the new one.
        /// </remarks>
        public static void Register<TFrom, TTo>(IConverter<TFrom, TTo> converter)
        {
            _converter.Register(converter);
        }
    }
}