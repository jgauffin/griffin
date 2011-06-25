using System;
using System.Collections.Generic;

namespace Griffin.Core.InversionOfControl
{
    public class ServiceMapping
    {
        private readonly List<Type> _implementations = new List<Type>();
        private Action<object> _factory;

        public ServiceMapping(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public Type ServiceType { get; set; }

        public Func<object> FactoryMethod { get; set; }

        public List<Type> Implementations
        {
            get { return _implementations; }
        }

        public void AddImplementation(Type concreteType)
        {
            _implementations.Add(concreteType);
        }
    }
}