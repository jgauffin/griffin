using System.Collections.Generic;

namespace Griffin.Core.Messaging
{
    internal interface IInvoker
    {
        IEnumerable<InvocationFailure> Invoke();
    }
}