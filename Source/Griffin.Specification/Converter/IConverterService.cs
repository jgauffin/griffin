using System;
using System.Diagnostics.Contracts;

namespace Griffin.Converter
{
    /// <summary>
    /// Framework used to convert between different object types.
    /// </summary>
    /// <remarks>
    /// Can be used to convert basic types like from a <c>string</c> to a <c>DateTime</c> or
    /// to map between a view model and a domain model.
    /// </remarks>
    [ContractClass(typeof(IConverterServiceContract))]
    public interface IConverterService
    {
        /// <summary>
        /// Convert from a type to another.
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="from">Object to convert</param>
        /// <returns>Created object</returns>
        TTo Convert<TFrom, TTo>(TFrom from);

        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="targetType">Type to convert to</param>
        /// <returns>Created object</returns>
        object Convert(object source, Type targetType);

        /// <summary>
        /// Register a new converter
        /// </summary>
        /// <typeparam name="TFrom">Type being converted</typeparam>
        /// <typeparam name="TTo">Type being created</typeparam>
        /// <param name="converter">Actual converter</param>
        /// <remarks>
        /// Any existing converter will be replaced with the new one.
        /// </remarks>
        void Register<TFrom, TTo>(IConverter<TFrom, TTo> converter);
    }
}