using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Net.Protocols.Http.Implementation.Headers
{
    class LazyHeader<T> : Lazy<T> where T : IHeader
    {
        
    }
}
