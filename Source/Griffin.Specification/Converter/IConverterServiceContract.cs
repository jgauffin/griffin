using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Griffin.Converter
{
    [ContractClassFor(typeof(IConverterService))]
    internal abstract class IConverterServiceContract : IConverterService
    {
        public TTo Convert<TFrom, TTo>(TFrom from)
        {
            return default(TTo);
        }

        public object Convert(object source, Type targetType)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(targetType != null);
            return null;
        }

        public void Register<TFrom, TTo>(IConverter<TFrom, TTo> converter)
        {
            Contract.Requires<ArgumentNullException>(converter != null);
        }
    }
}
