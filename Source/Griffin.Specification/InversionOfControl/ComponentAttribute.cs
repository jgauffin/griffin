using System;

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// A class that should be registered as a service in an inversion of control container.
    /// </summary>
    public class ComponentAttribute : Attribute
    {
        public ComponentAttribute()
        {
        }

        public ComponentAttribute(ComponentFlags flags)
        {
        }
    }

    public enum ComponentFlags
    {
        Singleton
    }
}