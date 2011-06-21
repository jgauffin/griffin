using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Multitenancy
{
    public interface ITenantAuthorizer
    {
        bool Authorize(ITenant tenant);
    }
}
