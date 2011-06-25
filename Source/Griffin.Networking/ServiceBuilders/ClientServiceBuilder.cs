using System;
using Griffin.Networking.Channels;
using Griffin.Networking.Pipelines;
using Griffin.Networking.Services;

namespace Griffin.Networking.ServiceBuilders
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
            return (T) Activator.CreateInstance(typeof (T), new[] {channel});
        }

        /// <summary>
        /// Configure service
        /// </summary>
        protected abstract void Configure(IServiceBuilderConfiguration configuration);
    }

    public class ServiceBuilderConfiguration : IServiceBuilderConfiguration
    {
        #region IServiceBuilderConfiguration Members

        public IChannelFactory ChannelFactory { get; set; }
        public IPipelineFactory PipelineFactory { get; set; }

        #endregion
    }
}