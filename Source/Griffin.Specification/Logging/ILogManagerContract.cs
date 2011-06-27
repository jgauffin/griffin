using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Griffin.Logging
{
// ReSharper disable InconsistentNaming
    [ContractClassFor(typeof(ILogManager))]
    internal abstract class ILogManagerContract : ILogManager
// ReSharper restore InconsistentNaming
    {
        public ILogger GetLogger(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Ensures(Contract.Result<ILogger>() != null);
            return null;
        }
    }
}
