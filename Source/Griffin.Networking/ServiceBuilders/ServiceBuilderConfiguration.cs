using Griffin.Networking.Channels;
using Griffin.Networking.Pipelines;

namespace Griffin.Networking.ServiceBuilders
{
    public interface IServiceBuilderConfiguration
    {
        IChannelFactory ChannelFactory { get; set; }
        IPipelineFactory PipelineFactory { get; set; }
    }
}