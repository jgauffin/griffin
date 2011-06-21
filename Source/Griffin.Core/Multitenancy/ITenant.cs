using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Multitenancy
{
    /// <summary>
    /// a tenant in the system
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// Gets the name of a the tenant
        /// </summary>
        string Name { get; }
    }
}
