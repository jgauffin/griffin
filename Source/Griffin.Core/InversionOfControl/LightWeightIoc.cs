using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Griffin.InversionOfControl;

namespace Griffin.Core.InversionOfControl
{
    /// <summary>
    /// This is not a very performant or flexible ioc.
    /// </summary>
    /// <remarks>
    /// <para>
    /// All objects are singletons in this container. You can't change that. Need something else?
    /// </para>
    /// <para>
    /// Use the <see cref="ParameterTypeRequested"/> to resolve any dependencies that the container
    /// cannot find by itself. Also use <see cref="ParameterInstanceRequested"/> to return parameters
    /// such as strings and primitives (or custom instances)
    /// </para>
    /// </remarks>
    public class LightWeightIoc : IServiceLocator
    {
        private readonly Dictionary<Type, Func<object>> _implementationFactories = new Dictionary<Type, Func<object>>();
        private readonly Dictionary<Type, ServiceMapping> _serviceRegistrations = new Dictionary<Type, ServiceMapping>();

        #region IServiceLocator Members

        public T Resolve<T>() where T : class
        {
            return (T) Build(typeof (T), BuildOptions.BuildRegistered);
        }

        public object Resolve(Type type)
        {
            return Build(type, BuildOptions.BuildRegistered);
        }

        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            throw new NotSupportedException("Sorry, collections are not supported yet.");
        }

        #endregion

        /// <summary>
        /// Build an object using the container
        /// </summary>
        /// <param name="requestedType">Type to build</param>
        /// <param name="buildOptions"></param>
        /// <returns></returns>
        public object Build(Type requestedType, BuildOptions buildOptions)
        {
            Type typeToBuild = null;

            // check if it's a service type.
            ServiceMapping mapping = GetServiceMapping(requestedType, false);
            if (mapping != null)
            {
                if (mapping.Implementations.Count > 1)
                    throw new InvalidOperationException("'" + requestedType.FullName +
                                                        "' got multiple implementations and can therefore not be requested.");
                typeToBuild = mapping.Implementations[0];
            }

            if (typeToBuild == null)
            {
                if (buildOptions == BuildOptions.BuildRegistered)
                    throw new InvalidOperationException("Service '" + requestedType +
                                                        "' is not registered in the container.");

                typeToBuild = requestedType;
            }

            // check if it's an created instance
            lock (_implementationFactories)
            {
                Func<object> factory;
                if (_implementationFactories.TryGetValue(typeToBuild, out factory))
                    return factory();
            }

            foreach (ConstructorInfo constructor in typeToBuild.GetConstructors())
            {
                Dictionary<string, Type> constructorInfo = ValidateConstructor(typeToBuild, constructor);
                if (constructorInfo == null)
                    continue;

                var parameters = new object[constructorInfo.Count];
                int index = 0;
                foreach (var parameterInfo in constructorInfo)
                {
                    object parameterInstance = null;

                    if (IsPrimitiveOrString(parameterInfo.Value))
                    {
                        var args = new ParameterInstanceEventArgs(typeToBuild, parameterInfo.Value, parameterInfo.Key);
                        ParameterInstanceRequested(this, args);
                        parameterInstance = args.Instance;
                    }

                    if (parameterInstance == null)
                    {
                        Func<object> factory;
                        lock (_implementationFactories)
                            _implementationFactories.TryGetValue(parameterInfo.Value, out factory);
                        parameterInstance = factory();
                    }


                    parameters[index++] = parameterInstance ?? Build(parameterInfo.Value, buildOptions);
                }

                object instance = Activator.CreateInstance(typeToBuild, parameters);
                if (!IsPrimitiveOrString(typeToBuild))
                {
                    lock (_implementationFactories)
                        _implementationFactories[typeToBuild] = () => instance;
                }
                return instance;
            }

            return null;
        }

        public static bool IsPrimitiveOrString(Type value)
        {
            return value.IsPrimitive || value == typeof (string);
        }

        public void Register<TService, TImplementation>() where TImplementation : TService where TService : class
        {
            ServiceMapping mapping = GetServiceMapping(typeof (TService), true);
            mapping.AddImplementation(typeof (TImplementation));
        }

        private ServiceMapping GetServiceMapping(Type type, bool create)
        {
            ServiceMapping mapping;
            lock (_serviceRegistrations)
            {
                if (!_serviceRegistrations.TryGetValue(type, out mapping))
                {
                    if (!create)
                        return null;

                    mapping = new ServiceMapping(type);
                    _serviceRegistrations.Add(type, mapping);
                }
            }

            return mapping;
        }

        public void RegisterFactoryMethod<TService>(Func<TService> factoryMethod)
        {
            ServiceMapping mapping = GetServiceMapping(typeof (TService), true);
            mapping.FactoryMethod = () => factoryMethod();
        }

        public void RegisterInstance<TService>(object instance)
        {
            ServiceMapping mapping = GetServiceMapping(typeof (TService), true);
            mapping.FactoryMethod = () => instance;
        }

        private Type ResolveType(Type type)
        {
            lock (_serviceRegistrations)
            {
                ServiceMapping mapping;
                if (_serviceRegistrations.TryGetValue(type, out mapping))
                {
                    return mapping.Implementations.Count == 1 ? mapping.Implementations[0] : null;
                }
            }

            return null;
        }

        private Type ResolveUsingEvent(Type typeToCreate, string parameterName, Type parameterType)
        {
            var args = new ParameterTypeEventArgs(typeToCreate, parameterType, parameterName);
            ParameterTypeRequested(this, args);
            return args.ImplementationType;
        }

        private Dictionary<string, Type> ValidateConstructor(Type typeToCreate, ConstructorInfo constructor)
        {
            var parameters = new Dictionary<string, Type>();
            foreach (ParameterInfo parameter in constructor.GetParameters())
            {
                Type parameterType = IsPrimitiveOrString(parameter.ParameterType)
                                         ? parameter.ParameterType
                                         : ResolveType(parameter.ParameterType);

                if (parameterType == null)
                {
                    parameterType = ResolveUsingEvent(typeToCreate, parameter.Name, parameter.ParameterType);
                    if (parameterType == null)
                        return null;
                }

                parameters.Add(parameter.Name, parameterType);
            }

            return parameters;
        }

        public IEnumerable ResolveAll(Type type)
        {
            throw new NotSupportedException("Sorry, collections are not supported yet.");
        }

        public event EventHandler<ParameterTypeEventArgs> ParameterTypeRequested = delegate { };
        public event EventHandler<ParameterInstanceEventArgs> ParameterInstanceRequested = delegate { };
    }

    public enum BuildOptions
    {
        BuildAny,
        BuildRegistered
    }
}