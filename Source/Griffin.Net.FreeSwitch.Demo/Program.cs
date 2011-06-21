using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Griffin.Core.Net.Protocols.FreeSwitch;

namespace Griffin.Net.FreeSwitch.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new FreeSwitchClientService("ClueCon");
            var builder = new FreeSwitchServiceBuilder(service);
            service.Connect(builder.CreateChannel(), new IPEndPoint(IPAddress.Loopback, 8021));
            Console.ReadLine();
        }
    }
}
