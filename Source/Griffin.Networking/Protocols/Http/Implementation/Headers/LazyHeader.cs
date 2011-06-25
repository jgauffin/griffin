using System;

namespace Griffin.Networking.Protocols.Http.Implementation.Headers
{
    internal class LazyHeader<T> : Lazy<T> where T : IHeader
    {
    }
}