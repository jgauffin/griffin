using System;
using System.Collections.Generic;
using System.Reflection;
using Griffin.Core.Reflection;
using Griffin.InversionOfControl;
using Griffin.Logging;

namespace Griffin.Core.InversionOfControl
{
    /// <summary>
    /// Scans through all assemblies (loaded and assemblies that will be loaded)
    /// </summary>
    /// <remarks>
    /// ReflectionBuilder is a small class which can use reflection to build components. All components that should
    /// be added to the container must be decorated with the [Component]
    /// </remarks>
    public abstract class ReflectionBuilder
    {
        private readonly IBuilderStrategy _builderStrategy;
        private readonly ILogger _logger = LogManager.GetLogger<IServiceLocator>();
        private readonly AssemblyObserver _observer = new AssemblyObserver();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionBuilder"/> class.
        /// </summary>
        protected ReflectionBuilder(IBuilderStrategy builderStrategy)
        {
            _builderStrategy = builderStrategy;
            _observer.AddAssemblyFilter(AssemblyObserver.OnlyInAppPath);
            _observer.AddTypeFilter(AssemblyObserver.OnlyConcreateClasses);
            _observer.AddTypeFilter(type => type.GetCustomAttributes(typeof (ComponentAttribute), false).Length > 0);
            _observer.TypeScanned += OnType;
        }

        /// <summary>
        /// Build a container using reflection and the <c>[Component]</c> attribute.
        /// </summary>
        public void Build()
        {
            _observer.Observe();
        }

        private void OnType(object sender, TypeScannedEventArgs e)
        {
            Add(e.FoundType);
        }


        /// <summary>
        /// Start all services which have implemented <see cref="IStartable"/>.
        /// </summary>
        /// <param name="locator">Locator used to find all startables.</param>
        /// <param name="context">Content being passed to all components. Use it to provide application specific information. Use <see cref="EmptyStartableContext"/> if you don't provide your own.</param>
        public void StartAll(IServiceLocator locator, IStartableContext context)
        {
            var visited = new List<object>();

            try
            {
                foreach (IStartable startable in locator.ResolveAll<IStartable>())
                {
                    if (visited.Contains(startable))
                        return;

                    startable.Start(context);
                    visited.Add(startable);
                }
            }
            catch (TargetInvocationException e)
            {
                throw e.PreserveStacktrace();
            }
        }

        /// <summary>
        /// Register all interfaces that the specified class implementss
        /// </summary>
        /// <param name="implementation">Implementation type</param>
        /// <param name="parameters">Optional contructors parameters</param>
        public void Add(Type implementation, params Parameter[] parameters)
        {
            _logger.Debug("Registering " + implementation);

            var items = new List<Type> {implementation};
            foreach (Type service in implementation.GetInterfaces())
            {
                if (service.Namespace.StartsWith("System"))
                    continue;
                items.Add(service);
            }
            if (parameters.Length > 0)
                _builderStrategy.RegisterType(implementation, items, parameters);
            else
                _builderStrategy.RegisterType(implementation, items);
        }


        /// <summary>
        /// Add a implementation
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="parameters">Specified constructor parameters (you don't have to specify all).</param>
        public void Add<TImplementation>(params Parameter[] parameters)
        {
            Add(typeof (TImplementation), parameters);
        }


        /// <summary>
        /// Add an existing instance.
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <param name="instance">Implementation</param>
        public void AddInstance<T>(T instance) where T : class
        {
            _builderStrategy.RegisterInstance(instance, typeof (T));
        }
    }
}