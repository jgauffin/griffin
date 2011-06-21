using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Pipelines;
using Griffin.Core.Net.ServiceBuilders;

namespace Griffin.Core.Net.Protocols.Http
{
    class ServerFactory : ServiceFactory, IPipelineFactory
    {
        /// <summary>
        /// Configure service
        /// </summary>
        protected override void Configure(IServiceBuilderConfiguration configuration)
        {
            //configuration.ChannelFactory = new TcpServerChannelFactory();
            //configuration.PipelineFactory = new 
        }

        /// <summary>
        /// Create a new pipeline with all attached channel handlers.
        /// </summary>
        public IPipeline CreatePipeline()
        {
            return null;
        }
    }
}
