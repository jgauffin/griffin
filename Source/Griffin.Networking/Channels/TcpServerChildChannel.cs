using Griffin.Networking.Pipelines;

namespace Griffin.Networking.Channels
{
    /// <summary>
    /// Channel for an accepted endpoint in a server.
    /// </summary>
    internal class TcpServerChildChannel : TcpChannel
    {
        public TcpServerChildChannel(IPipeline pipeline) : base(pipeline)
        {
        }
    }
}