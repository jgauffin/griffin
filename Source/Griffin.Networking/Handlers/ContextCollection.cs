using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Messages;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.Handlers
{
    public class ContextCollection : IContextCollection
    {
        private readonly IChannel _channel;

        private readonly Dictionary<IChannelHandler, ChannelHandlerContext> _contexts =
            new Dictionary<IChannelHandler, ChannelHandlerContext>();

        public ContextCollection(IChannel channel)
        {
            _channel = channel;
        }

        /// <summary>
        /// Each handler should have it's own context (per channel) to be able to 
        /// store information in it.
        /// </summary>
        private void CreateContexts()
        {
            if (!_channel.Pipeline.DownstreamHandlers.Any() || !_channel.Pipeline.UpstreamHandlers.Any())
                throw new InvalidOperationException("Pipeline has not been properly configured.");

            // need to create one context per handler
            List<IUpstreamHandler> upstream = _channel.Pipeline.UpstreamHandlers.ToList();
            List<IDownstreamHandler> downstream = _channel.Pipeline.DownstreamHandlers.ToList();

            for (int i = 0; i < upstream.Count; i++)
            {
                var ctx = new ChannelHandlerContext(_channel, this) {Name = upstream[i].GetType().Name};
                _contexts[upstream[i]] = ctx;

                if (i + 1 < upstream.Count)
                    ctx.NextUpstreamHandler = upstream[i + 1];
            }
            for (int i = 0; i < downstream.Count; i++)
            {
                ChannelHandlerContext ctx;
                if (!_contexts.TryGetValue(downstream[i], out ctx))
                {
                    ctx = new ChannelHandlerContext(_channel, this) { Name = downstream[i].GetType().Name };
                    _contexts.Add(downstream[i], ctx);
                }

                if (i + 1 < downstream.Count)
                    ctx.NextDownstreamHandler = downstream[i + 1];
            }

            // always add the channel as the last downstream handler.
            _contexts[_channel.Pipeline.DownstreamHandlers.Last()].NextDownstreamHandler = _channel;
            _contexts[_channel] = new ChannelHandlerContext(_channel, this); 
        }

        /// <summary>
        /// Send a message down stream
        /// </summary>
        /// <param name="channelEvent">Event to send</param>
        public void SendDownstream(IChannelEvent channelEvent)
        {
            SendDownstream(_channel.Pipeline.DownstreamHandlers.First(), channelEvent);
        }

        /// <summary>
        /// Send a message down stream (from application to channel)
        /// </summary>
        /// <param name="handler">Handler to start with</param>
        /// <param name="channelEvent">Event to send</param>
        /// <remarks>
        /// All handlers above the specified one will be ignored and not invoked.
        /// </remarks>
        public void SendDownstream(IDownstreamHandler handler, IChannelEvent channelEvent)
        {
            var ctx = _contexts[handler];
            handler.HandleDownstream(ctx, channelEvent);
        }

        /// <summary>
        /// Send an event upstream (from channels to application)
        /// </summary>
        /// <param name="channelEvent">Event to send</param>
        public void SendUpstream(IChannelEvent channelEvent)
        {
            SendUpstream(_channel.Pipeline.UpstreamHandlers.First(), channelEvent);
        }

        /// <summary>
        /// Send an event upstream (from channels to application)
        /// </summary>
        /// <param name="handlerToStartFrom">Handler that should be invoked</param>
        /// <param name="channelEvent">Event to send</param>
        public void SendUpstream(IUpstreamHandler handlerToStartFrom, IChannelEvent channelEvent)
        {
            var ctx = _contexts[handlerToStartFrom];
            handlerToStartFrom.HandleUpstream(ctx, channelEvent);
        }

        /// <summary>
        /// Get context for a specific channel
        /// </summary>
        /// <param name="value"></param>
        public IChannelHandlerContext Get(IChannelHandler value)
        {
            return _contexts[value];
        }

        public void Initialize()
        {
            CreateContexts();
        }
    }
}
