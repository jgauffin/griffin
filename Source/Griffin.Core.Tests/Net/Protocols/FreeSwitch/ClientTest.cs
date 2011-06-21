using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Griffin.Core.Net.Protocols.FreeSwitch;
using Xunit;

namespace Griffin.Core.Tests.Net.Protocols.FreeSwitch
{
    public class ClientTest
    {
        [Fact]
        public void Test()
        {
            var service = new FreeSwitchClientService("ClueCon");
            var builder = new FreeSwitchServiceBuilder(service);
            service.Connect(builder.CreateChannel(), new IPEndPoint(IPAddress.Loopback, 8021));
            Thread.Sleep(5000);
        }
    }
}
