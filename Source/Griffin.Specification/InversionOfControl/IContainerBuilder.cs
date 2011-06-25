using System;

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// Used to build an inversion of control container.
    /// </summary>
    /// <remarks>
    /// All instances should be considered to be scoped
    /// </remarks>
    public interface IContainerBuilder
    {
        /// <summary>
        /// Register a specific service
        /// </summary>
        /// <typeparam name="TService">Service to register</typeparam>
        /// <typeparam name="TImplementation">Class that provides the specified service.</typeparam>
        void Register<TService, TImplementation>();

        void Register(Type service, Type implementation);

        void Register<TService>(Func<TService> factoryMethod);
        void RegisterInstance<TService>(TService implementation);
    }
}