using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Griffin.Logging.Filters
{
    [ContractClassFor(typeof(IPreFilter))]
    internal abstract class IPreFilterContract : IPreFilter
    {
        public bool CanLog(Type loggedType, LogLevel logLevel)
        {
            Contract.Requires(loggedType != null);
            return false;
        }
    }
}
