﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Griffin.Converter;

namespace Griffin.Core.Converters
{
    /// <summary>
    /// Default implementation for the converter.
    /// </summary>
    public class DefaultConverterService : IConverterService
    {
        private readonly Dictionary<Type, Dictionary<Type, ConverterWrapper>> _converters =
            new Dictionary<Type, Dictionary<Type, ConverterWrapper>>();

        private readonly LinkedList<IConverterProvider> _providers = new LinkedList<IConverterProvider>();

        private ConverterWrapper FindConverterInProviders(Type targetType, MethodInfo providerMethod)
        {
            foreach (IConverterProvider provider in _providers)
            {
                object converter = providerMethod.Invoke(provider, null);
                if (converter == null)
                    continue;

                return new ConverterWrapper(converter, targetType);
            }

            return null;
        }

        private static object TryDefaultConversion(object source, Type targetType)
        {
            try
            {
                return System.Convert.ChangeType(source, targetType);
            }
            catch (InvalidCastException exception)
            {
                throw new NotSupportedException(
                    "There are no registered converter from {0} to {1}.".FormatWith(source.GetType().FullName,
                                                                                    targetType.FullName), exception);
            }
        }

        #region IConverterService Members

        /// <summary>
        /// Convert from a type to another.
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="from">Object to convert</param>
        /// <returns>Created object</returns>
        public TTo Convert<TFrom, TTo>(TFrom from)
        {
            return (TTo) Convert(from, typeof (TFrom));
        }

        /// <summary>
        /// Converts all.
        /// </summary>
        /// <typeparam name="TFrom">The type to convert from.</typeparam>
        /// <typeparam name="TTo">The type to convert to.</typeparam>
        /// <param name="collection">Items to convert.</param>
        /// <returns>A collection of converted items</returns>
        public IEnumerable<TTo> ConvertAll<TFrom, TTo>(IEnumerable<TFrom> collection)
        {
            return collection.Select(Convert<TFrom, TTo>).ToList();
        }

        /// <summary>
        /// Convert from one type to another.
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="targetType">Type to convert to</param>
        /// <returns>Created object</returns>
        public object Convert(object source, Type targetType)
        {
            Dictionary<Type, ConverterWrapper> items;
            MethodInfo providerMethod = typeof (IConverterProvider).GetMethod("Get").MakeGenericMethod(
                source.GetType(), targetType);
            if (!_converters.TryGetValue(source.GetType(), out items))
            {
                items = new Dictionary<Type, ConverterWrapper>();
                _converters.Add(source.GetType(), items);

                ConverterWrapper converter = FindConverterInProviders(targetType, providerMethod);
                if (converter != null)
                    items[targetType] = converter;
                else
                    return TryDefaultConversion(source, targetType);
            }

            ConverterWrapper method;
            if (items.TryGetValue(targetType, out method))
                return method.Convert(source);

            method = FindConverterInProviders(targetType, providerMethod);
            if (method != null)
                items.Add(targetType, method);

            return method == null
                       ? TryDefaultConversion(source, targetType)
                       : method.Convert(source);
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
        public void Register<TFrom, TTo>(IConverter<TFrom, TTo> converter)
        {
            Dictionary<Type, ConverterWrapper> items;
            if (!_converters.TryGetValue(typeof (TFrom), out items))
            {
                items = new Dictionary<Type, ConverterWrapper>();
                _converters.Add(typeof (TFrom), items);
            }

            items[typeof (TTo)] = new ConverterWrapper(converter, typeof (TFrom));
        }

        public void Register(IConverterProvider provider)
        {
            _providers.AddLast(provider);
        }

        #endregion

        #region Nested type: ConverterWrapper

        private class ConverterWrapper
        {
            public ConverterWrapper(object converter, Type fromType)
            {
                Converter = converter;
                Method = converter.GetType().GetMethod("Convert", new[] {fromType});
            }

            private object Converter { get; set; }
            private MethodBase Method { get; set; }

            public object Convert(object source)
            {
                return Method.Invoke(Converter, new[] {source});
            }
        }

        #endregion
    }
}