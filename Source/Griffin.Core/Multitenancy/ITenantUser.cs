using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Multitenancy
{
    /// <summary>
    /// Currently logged in user for a hosted tenant
    /// </summary>
    public interface ITenantUser
    {
        string FullName { get; }

    }
}
