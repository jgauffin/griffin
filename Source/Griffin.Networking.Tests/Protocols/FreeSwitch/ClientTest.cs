using System.Net;
using System.Threading;
using Griffin.Networking.Protocols.FreeSwitch;
using Xunit;

namespace Griffin.Networking.Tests.Protocols.FreeSwitch
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