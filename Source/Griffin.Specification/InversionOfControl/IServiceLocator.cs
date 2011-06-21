using System;
using System.Collections.Generic;

namespace Griffin.Specification.InversionOfControl
{
    /// <summary>
    /// Used to locate registered services.
    /// </summary>
    /// <seealso cref="IContainerBuilder"/>.
    public interface IServiceLocator
    {
        /// <summary>
        /// Find all registered instances of a type
        /// </summary>
        /// <typeparam name="T">Type of service to locate</typeparam>
        /// <returns>All found implemententations or an empty collection.</returns>
        IEnumerable<T> FindAll<T>() where T : class;

        /// <summary>
        /// Locate a service.
        /// </summary>
        /// <typeparam name="T">Type of service to locate</typeparam>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        T Get<T>() where T : class;

        /// <summary>
        /// Locate a service.
        /// </summary>
        /// <param name="serviceType">Type of service to locate</param>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        object Get(Type serviceType);
    }
}