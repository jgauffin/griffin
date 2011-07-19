using System;
using System.Linq;
using System.Text;
using Griffin.Messaging;

namespace Griffin.Core.Tests.Messaging
{

    public class FindContactsRequest : IRequest
    {
        public string SearchValue { get; set; }
    }
}
