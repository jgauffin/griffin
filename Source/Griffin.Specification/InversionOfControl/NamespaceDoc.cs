using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.InversionOfControl
{
    /// <summary>
    /// Defines how an inversion of control container should be implemented.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This implementation have to different parts. The first one is the container build which is used to register
    /// all types and the services that they implement. The <see cref="IContainerBuilder"/> then builds the
    /// actual <see cref="IServiceLocator"/> which is used to locate services.
    /// </para>
    /// </remarks>
    class NamespaceDoc
    {
    }
}
