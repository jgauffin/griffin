using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Pipelines;
using Griffin.Core.Net.ServiceBuilders;

namespace Griffin.Core.Net.Protocols.FreeSwitch
{
    public class FreeSwitchServiceBuilder : ServiceFactory, IPipelineFactory
    {
        private readonly FreeSwitchClientService _clientService;

        public FreeSwitchServiceBuilder(FreeSwitchClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Configures the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        protected override void Configure(IServiceBuilderConfiguration configuration)
        {
            configuration.ChannelFactory = new TcpClientChannelFactory();
            configuration.PipelineFactory = this;
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
    }
}