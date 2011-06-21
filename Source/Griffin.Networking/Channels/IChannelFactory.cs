using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.Channels
{
    public interface IChannelFactory
    {
        IChannel CreateChannel(IPipeline pipeline);
    }
}