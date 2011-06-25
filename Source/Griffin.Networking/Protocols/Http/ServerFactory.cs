using Griffin.Networking.Pipelines;
using Griffin.Networking.ServiceBuilders;

namespace Griffin.Networking.Protocols.Http
{
    internal class ServerFactory : ServiceFactory, IPipelineFactory
    {
        #region IPipelineFactory Members

        /// <summary>
        /// Create a new pipeline with all attached channel handlers.
        /// </summary>
        public IPipeline CreatePipeline()
        {
            return null;
        }

        #endregion

        /// <summary>
        /// Configure service
        /// </summary>
        protected override void Configure(IServiceBuilderConfiguration configuration)
        {
            //configuration.ChannelFactory = new TcpServerChannelFactory();
            //configuration.PipelineFactory = new 
        }
    }
}