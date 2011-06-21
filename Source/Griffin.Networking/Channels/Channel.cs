using System;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Messages;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.Channels
{
    public abstract class Channel : IChannel
    {
        private readonly ContextCollection _contexts;

        protected Channel(IPipeline pipeline)
        {
            Pipeline = pipeline;
            _contexts = new ContextCollection(this);
        
        }
        
        protected virtual void Initialize()
        {
            _contexts.Initialize();
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

        protected abstract void UpstreamHandlingComplete(IChannelEvent e);
        protected abstract void DownstreamHandlingComplete(IChannelEvent e);

        void IDownstreamHandler.HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            DownstreamHandlingComplete(e);
        }

        #endregion
    }
}