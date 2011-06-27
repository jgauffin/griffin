using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// Used to locate registered services.
    /// </summary>
    /// <seealso cref="IContainerBuilder"/>.
    [ContractClass(typeof(IServiceLocatorContract))]
    public interface IServiceLocator
    {
        /// <summary>
        /// Find all registered instances of a type
        /// </summary>
        /// <typeparam name="T">Type of service to locate</typeparam>
        /// <returns>All found implemententations or an empty collection.</returns>
        IEnumerable<T> ResolveAll<T>() where T : class;

        /// <summary>
        /// Locate a service.
        /// </summary>
        /// <typeparam name="T">Type of service to locate</typeparam>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// Locate a service.
        /// </summary>
        /// <param name="serviceType">Type of service to locate</param>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        object Resolve(Type serviceType);
    }
}