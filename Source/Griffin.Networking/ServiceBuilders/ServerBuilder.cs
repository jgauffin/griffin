using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.ServiceBuilders
{
    abstract class ServerBuilder
    {
        protected ServerConfiguration Configuration { get; set; }

        protected ServerBuilder()
        {
            Configure(Configuration);
        }

        public IChannel CreateClientChannel()
        {
            return null;
            IPipeline pipeline = Configuration.ClientPipelineFactory.CreatePipeline();
            //return Configuration.CreateChannel(pipeline);
        }

        public IChannel CreateServerChannel()
        {
            return null;
        }


        /// <summary>
        /// Configure service
        /// </summary>
        protected abstract void Configure(ServerConfiguration configuration);
    }
}
