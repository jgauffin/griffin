using System;
using System.Diagnostics.Contracts;

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// Used to build an inversion of control container.
    /// </summary>
    /// <remarks>
    /// <para>
    /// All instances should be considered to be scoped. What the scope is depends on the type
    /// of application. For a web application it should be for the lifetime of an request/response,
    /// while for an winforms application or a windows service it's for the lifetime of the application.
    /// </para>
    /// <para>
    /// It will of course depends on the implementation that you use.
    /// </para>
    /// </remarks>
    [ContractClass(typeof(IContainerBuilderContract))]
    public interface IContainerBuilder
    {
        /// <summary>
        /// Register a service
        /// </summary>
        /// <typeparam name="TService">Service to register</typeparam>
        /// <typeparam name="TImplementation">Class that provides the specified service.</typeparam>
        void Register<TService, TImplementation>();

        /// <summary>
        /// Register a service
        /// </summary>
        /// <param name="service">Service to register</param>
        /// <param name="implementation">Class that provides the service.</param>
        void Register(Type service, Type implementation);

        /// <summary>
        /// Register a service
        /// </summary>
        /// <typeparam name="TService">Service to register</typeparam>
        /// <param name="factoryMethod">Method creating the service</param>
        void Register<TService>(Func<TService> factoryMethod);

        /// <summary>
        /// Register a previously created instance.
        /// </summary>
        /// <typeparam name="TService">Service to register</typeparam>
        /// <param name="implementation">Created instance</param>
        void RegisterInstance<TService>(TService implementation) where TService : class;

        /// <summary>
        /// Register an instance
        /// </summary>
        /// <param name="service">Type of service</param>
        /// <param name="instance">Instance implementing the service</param>
        void RegisterInstance(Type service, object instance);

        /// <summary>
        /// Build the service locator
        /// </summary>
        /// <returns>Created container</returns>
        IServiceLocator Build();
    }
}