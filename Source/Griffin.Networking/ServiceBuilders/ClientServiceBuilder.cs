using System;
using System.Net;
using System.Reflection;
using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Pipelines;
using Griffin.Core.Net.Services;

namespace Griffin.Core.Net.ServiceBuilders
{
    public abstract class ClientServiceBuilder<T> where T : ClientService
    {
        protected IServiceBuilderConfiguration Configuration { get; set; }

        public T Build()
        {
            Configuration = new ServiceBuilderConfiguration();
            Configure(Configuration);
            IPipeline pipeline = Configuration.PipelineFactory.CreatePipeline();
            IChannel channel = Configuration.ChannelFactory.CreateChannel(pipeline);
            return (T)Activator.CreateInstance(typeof(T), new []{channel});
        }

        /// <summary>
        /// Configure service
        /// </summary>
        protected abstract void Configure(IServiceBuilderConfiguration configuration);
    }

    public class ServiceBuilderConfiguration : IServiceBuilderConfiguration
    {
        public IChannelFactory ChannelFactory { get; set; }
        public IPipelineFactory PipelineFactory { get; set; }
    }
}