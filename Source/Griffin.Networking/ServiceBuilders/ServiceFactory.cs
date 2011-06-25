using Griffin.Networking.Channels;
using Griffin.Networking.Pipelines;

namespace Griffin.Networking.ServiceBuilders
{
    /// <summary>
    /// Used to build all components needed to create a network server/client.
    /// </summary>
    public abstract class ServiceFactory
    {
        protected ServiceFactory()
        {
            Configuration = new ServiceBuilderConfiguration();
            Configure(Configuration);
        }

        protected IServiceBuilderConfiguration Configuration { get; set; }

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