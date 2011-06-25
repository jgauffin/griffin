using Griffin.Networking.Channels;
using Griffin.Networking.Pipelines;

namespace Griffin.Networking.ServiceBuilders
{
    internal abstract class ServerBuilder
    {
        protected ServerBuilder()
        {
            Configure(Configuration);
        }

        protected ServerConfiguration Configuration { get; set; }

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