using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Griffin.Logging.Filters
{

    [ContractClassFor(typeof(IPostFilter))]
// ReSharper disable InconsistentNaming
    internal abstract class IPostFilterContract : IPostFilter
// ReSharper restore InconsistentNaming
    {
        public bool CanLog(LogEntry entry)
        {
            Contract.Requires(entry != null);
            return false;
        }
    }
}
