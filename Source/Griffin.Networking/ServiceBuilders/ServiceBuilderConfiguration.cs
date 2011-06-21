using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.ServiceBuilders
{
    public interface IServiceBuilderConfiguration
    {
        IChannelFactory ChannelFactory { get; set; }
        IPipelineFactory PipelineFactory { get; set; }
    }
}