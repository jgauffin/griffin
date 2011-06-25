using Griffin.Networking.Channels;
using Griffin.Networking.Pipelines;
using Griffin.Networking.ServiceBuilders;

namespace Griffin.Networking.Protocols.FreeSwitch
{
    public class FreeSwitchServiceBuilder : ServiceFactory, IPipelineFactory
    {
        private readonly FreeSwitchClientService _clientService;

        public FreeSwitchServiceBuilder(FreeSwitchClientService clientService)
        {
            _clientService = clientService;
        }

        #region IPipelineFactory Members

        /// <summary>
        /// Create a new pipeline with all attached channel handlers.
        /// </summary>
        IPipeline IPipelineFactory.CreatePipeline()
        {
            var pipeline = new Pipeline();
            pipeline.RegisterUpstreamHandler(new Decoder());
            pipeline.RegisterUpstreamHandler(_clientService);
            pipeline.RegisterDownstreamHandler(new Encoder());
            return pipeline;
        }

        #endregion

        /// <summary>
        /// Configures the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        protected override void Configure(IServiceBuilderConfiguration configuration)
        {
            configuration.ChannelFactory = new TcpClientChannelFactory();
            configuration.PipelineFactory = this;
        }
    }
}