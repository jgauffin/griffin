using System;
using System.Diagnostics.Contracts;

namespace Griffin.InversionOfControl
{
    [ContractClassFor(typeof (IContainerBuilder))]
// ReSharper disable InconsistentNaming
    internal abstract class IContainerBuilderContract : IContainerBuilder
// ReSharper restore InconsistentNaming
    {
        #region IContainerBuilder Members

        public void Register<TService, TImplementation>()
        {
        }

        public void Register(Type service, Type implementation)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentNullException>(implementation != null);
        }

        public void Register<TService>(Func<TService> factoryMethod)
        {
            Contract.Requires<ArgumentNullException>(factoryMethod != null);
        }

        public void RegisterInstance<TService>(TService implementation) where TService : class
        {
            Contract.Requires<ArgumentNullException>(implementation != default(TService));
        }

        public IServiceLocator Build()
        {
// ReSharper disable InvocationIsSkipped
            Contract.Ensures(Contract.Result<IServiceLocator>() != null);
// ReSharper restore InvocationIsSkipped
            return null;
        }

        #endregion
    }
}