using System;
using System.Collections.Generic;
using System.Reflection;
using Griffin.InversionOfControl;
using Griffin.Logging;

namespace Griffin.Core.InversionOfControl
{
    public abstract class ReflectionBuilder
    {
        private readonly AssemblyLoader _loader = new AssemblyLoader();
        private readonly ILogger _logger = LogManager.GetLogger<IServiceLocator>();
        private IContainerBuilder _builder;
        private IServiceLocator _serviceResolver;

        protected ReflectionBuilder()
        {
            _loader.TypeScanned += OnType;
        }

        internal IContainerBuilder ContainerBuilder
        {
            get { return _builder; }
        }

        public void Build()
        {
            _loader.LoadAllTypes();
        }

        private void OnType(object sender, TypeScannedEventArgs e)
        {
            Type componentAttribute = typeof (ComponentAttribute);
            object[] attrs = e.FoundType.GetCustomAttributes(componentAttribute, false);
            if (attrs.Length != 1)
                return;

            Add(e.FoundType);
        }


        public void StartAll(IStartableContext context)
        {
            var visited = new List<object>();

            try
            {
                foreach (IStartable startable in _serviceResolver.ResolveAll<IStartable>())
                {
                    if (visited.Contains(startable))
                        return;

                    startable.Start(context);
                    visited.Add(startable);
                }
            }
            catch (TargetInvocationException e)
            {
                throw e.FixStacktrace();
            }
        }

        public void Add(Type implementation, params Parameter[] parameters)
        {
            _logger.Debug("Registering " + implementation);

            var items = new List<Type> {implementation};
            foreach (Type @interface in implementation.GetInterfaces())
            {
                if (@interface.Namespace.StartsWith("System"))
                    continue;
                items.Add(@interface);
            }
            if (parameters.Length > 0)
                RegisterType(implementation, items, parameters);
            else
                RegisterType(implementation, items);
        }

        public abstract void RegisterType(Type implementation, IEnumerable<Type> services);
        public abstract void RegisterType(Type implementation, IEnumerable<Type> services, Parameter[] parameters);
        public abstract void RegisterInstance(object instance, Type service);

        public void Add<TImplementation>(params Parameter[] parameters)
        {
            Add(typeof (TImplementation), parameters);
        }


        public void AddInstance<T>(T instance) where T : class
        {
            RegisterInstance(instance, typeof (T));
        }
    }
}