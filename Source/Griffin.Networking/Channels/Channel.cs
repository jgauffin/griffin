using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;
using Griffin.Networking.Pipelines;

namespace Griffin.Networking.Channels
{
    public abstract class Channel : IChannel
    {
        private readonly ContextCollection _contexts;

        protected Channel(IPipeline pipeline)
        {
            Pipeline = pipeline;
            _contexts = new ContextCollection(this);
        }

        #region IChannel Members

        /// <summary>
        /// Gets pipeline that everything 
        /// </summary>
        public IPipeline Pipeline { get; private set; }

        /// <summary>
        /// Gets contexts used for each handler in a channel.
        /// </summary>
        public IContextCollection HandlerContexts
        {
            get { return _contexts; }
        }

        /// <summary>
        /// Process data that was received from the channel.
        /// </summary>
        /// <param name="ctx">Context which is used for the currently processed channel</param>
        /// <param name="e">Event being processed.</param>
        void IUpstreamHandler.HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            UpstreamHandlingComplete(e);
        }

        void IDownstreamHandler.HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            DownstreamHandlingComplete(e);
        }

        #endregion

        protected virtual void Initialize()
        {
            _contexts.Initialize();
        }

        protected abstract void UpstreamHandlingComplete(IChannelEvent e);
        protected abstract void DownstreamHandlingComplete(IChannelEvent e);
    }
}