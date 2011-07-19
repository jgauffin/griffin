/*
 * Copyright (c) 2011, Jonas Gauffin. All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301 USA
 */
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
    /// It will of course depends on the implementation that you use.
    /// </para>
    /// <para>
    /// All implementations should return the same instance for all services that are registered as scoped/singletons for
    /// a specific class. In other words, the following code should return the same instance:
    /// <example>
    /// <code>
    /// <![CDATA[
    /// builder.Register<IUserService, UserService>();
    /// builder.Register<IStartable, UserService>();
    /// 
    /// var container = builder.Build();
    /// var service = container.Resolve<IUserService>();
    /// var startable = container.ResolveAll<IStartable>().First();
    /// if (service != startable)
    ///   throw new InvalidOperationException("Same instance should have been returned");
    /// ]]>
    /// </code>
    /// </example>
    /// </para>
    /// </remarks>
    /// <seealso cref="ServiceFeatureAttribute"/>
    [ContractClass(typeof(IContainerBuilderContract))]
    public interface IContainerBuilder
    {
        /// <summary>
        /// Register a service
        /// </summary>
        /// <typeparam name="TService">Service to register</typeparam>
        /// <typeparam name="TImplementation">Class that provides the specified service.</typeparam>
        /// <param name="flags">Defines how the service should be created</param>
        void Register<TService, TImplementation>(ComponentFlags flags = ComponentFlags.Scoped);

        /// <summary>
        /// Register a service
        /// </summary>
        /// <param name="service">Service to register</param>
        /// <param name="implementation">Class that provides the service.</param>
        /// <param name="flags">Defines how the service should be created</param>
        void Register(Type service, Type implementation, ComponentFlags flags = ComponentFlags.Scoped);

        /// <summary>
        /// Register a service
        /// </summary>
        /// <typeparam name="TService">Service to register</typeparam>
        /// <param name="factoryMethod">Method creating the service</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///     builder.Register<IUserService>(() => proxyGenerator.CreateService<IUserService>());
        /// ]]>
        /// </code>
        /// </example>
        void Register<TService>(Func<TService> factoryMethod);

        /// <summary>
        /// Register a previously created instance.
        /// </summary>
        /// <typeparam name="TService">Service to register</typeparam>
        /// <param name="implementation">Created instance</param>
        void RegisterInstance<TService>(TService implementation) where TService : class;

        /// <summary>
        /// Build a new service locator
        /// </summary>
        /// <returns>Created container</returns>
        IServiceLocator Build();

        /// <summary>
        /// Update an existing container with the configuration in this builder.
        /// </summary>
        /// <param name="locator">Locator to update</param>
        void Update(IServiceLocator locator);
    }
}