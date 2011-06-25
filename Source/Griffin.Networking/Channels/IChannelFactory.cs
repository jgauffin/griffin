using Griffin.Networking.Pipelines;

namespace Griffin.Networking.Channels
{
    public interface IChannelFactory
    {
        IChannel CreateChannel(IPipeline pipeline);
    }
}