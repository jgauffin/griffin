using System.Collections.Generic;
using Griffin.Messaging;

namespace Griffin.Core.Tests.Messaging
{
    public class FindContactsResponse : IResponse
    {
        public ICollection<string> Contacts { get; set; }
    }
}