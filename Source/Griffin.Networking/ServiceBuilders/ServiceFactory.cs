using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Pipelines;
using Griffin.Core.Net.Services;

namespace Griffin.Core.Net.ServiceBuilders
{
    /// <summary>
    /// Used to build all components needed to create a network server/client.
    /// </summary>
    public abstract class ServiceFactory
    {
        protected IServiceBuilderConfiguration Configuration { get; set; }

        protected ServiceFactory()
        {
            Configuration = new ServiceBuilderConfiguration();
            Configure(Configuration);
        }

        public IChannel CreateChannel()
        {
            IPipeline pipeline = Configuration.PipelineFactory.CreatePipeline();
            return Configuration.ChannelFactory.CreateChannel(pipeline);
        }


        /// <summary>
        /// Configure service
        /// </summary>
        protected abstract void Configure(IServiceBuilderConfiguration configuration);
    }

}