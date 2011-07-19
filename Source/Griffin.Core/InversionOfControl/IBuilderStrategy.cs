using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.InversionOfControl;

namespace Griffin.Core.InversionOfControl
{
    /// <summary>
    /// Strategy used to register all components that is found by <see cref="ReflectionBuilder"/>
    /// </summary>
    public interface IBuilderStrategy
    {
        void RegisterType(Type implementation, IEnumerable<Type> services);
        void RegisterType(Type implementation, IEnumerable<Type> services, Parameter[] parameters);
        void RegisterInstance(object instance, Type service);
    }
}
