using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Griffin.InversionOfControl
{
#pragma warning disable 1591
// ReSharper disable InconsistentNaming
    [ContractClassFor(typeof(IServiceLocator))]
    internal abstract class IServiceLocatorContract : IServiceLocator
// ReSharper restore InconsistentNaming
#pragma warning restore 1591
    {
        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
            return null;
        }

        public T Resolve<T>() where T : class
        {
            return null;
        }

        public object Resolve(Type serviceType)
        {
            Contract.Requires<ArgumentNullException>(serviceType != null);
            return null;
        }
    }
}
