using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// Defines that the interface is a feature and not a real service.
    /// </summary>
    /// <remarks>
    /// Some Inversion Of Control containers (like Unity) do not support that the same interface
    /// is registered for multiple implementations without using unique names. This interface can be 
    /// used to get around that problem. Each <see cref="IContainerBuilder"/> implementation should
    /// check if the registered service got this interface. If true, the builder should register
    /// the service using a random unique name (and thus adding support to fetch multiple implementations
    /// without having to specify names).
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface)]
    public class ServiceFeatureAttribute : Attribute
    {
    }
}
