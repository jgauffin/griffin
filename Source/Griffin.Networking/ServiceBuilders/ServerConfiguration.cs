using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.ServiceBuilders
{
    public class ServerConfiguration
    {
        /// <summary>
        /// Gets or sets factory used to create a client pipeline
        /// </summary>
        public IPipelineFactory ClientPipelineFactory { get; set; }

        /// <summary>
        /// Gets or sets factory used to create a server pipeline
        /// </summary>
        public IPipelineFactory ServerPipelineFactory { get; set; }


    }
}
