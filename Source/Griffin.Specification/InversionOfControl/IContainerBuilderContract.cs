using System;
using System.Diagnostics.Contracts;

namespace Griffin.InversionOfControl
{
    [ContractClassFor(typeof (IContainerBuilder))]
    internal abstract class IContainerBuilderContract : IContainerBuilder
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
            Contract.Ensures(Contract.Result<IServiceLocator>() != null);
            return null;
        }

        #endregion
    }
}