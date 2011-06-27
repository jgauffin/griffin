using System;

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// A class that should be registered as a service in an inversion of control container.
    /// </summary>
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentAttribute"/> class.
        /// </summary>
        public ComponentAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentAttribute"/> class.
        /// </summary>
        /// <param name="flags">Defines object type.</param>
        public ComponentAttribute(ComponentFlags flags)
        {
            Flags = flags;
        }

        /// <summary>
        /// Gets flags defining how to instantiate the object etc.
        /// </summary>
        public ComponentFlags Flags { get; private set; }
    }
}