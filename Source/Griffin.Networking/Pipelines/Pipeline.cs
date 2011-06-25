using System;
using System.Collections.Generic;
using Griffin.Networking.Channels;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;

namespace Griffin.Networking.Pipelines
{
    public class Pipeline : IPipeline
    {
        private readonly LinkedList<IDownstreamHandler> _downstreamHandlers = new LinkedList<IDownstreamHandler>();
        private readonly LinkedList<IUpstreamHandler> _upstreamHandlers = new LinkedList<IUpstreamHandler>();

        #region IPipeline Members

        public void RegisterUpstreamHandler(IUpstreamHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            _upstreamHandlers.AddLast(handler);
        }

        public void RegisterDownstreamHandler(IDownstreamHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            if (handler is IChannel)
                throw new InvalidOperationException(
                    "Channels will be registered by the framework, do not add them to the pipeline");

            _downstreamHandlers.AddLast(handler);
        }

        public IEnumerable<IDownstreamHandler> DownstreamHandlers
        {
            get { return _downstreamHandlers; }
        }

        public IEnumerable<IUpstreamHandler> UpstreamHandlers
        {
            get { return _upstreamHandlers; }
        }

        #endregion

        public void SendDownstream(IChannel source, IChannelEvent channelEvent)
        {
            source.HandlerContexts.SendDownstream(channelEvent);
            /*
            LinkedListNode<IDownstreamHandler> node = _downstreamHandlers.First;
            while (node != null)
            {
                if (node.Value == source)
                    break;

                node = node.Next;
            }

            if (node == null)
                throw new InvalidOperationException("Failed to find handler " + node.Value.GetType().FullName + " in the ppeline.");

            var context = source.HandlerContexts.Get(node.Value);
            node.Value.HandleDownstream(context, channelEvent);
            */
        }

        public void SendUpstream(IChannel source, IChannelEvent channelEvent)
        {
            source.HandlerContexts.SendUpstream(channelEvent);
            /*
            LinkedListNode<IUpstreamHandler> node = _upstreamHandlers.First;
            while (node != null)
            {
                if (node.Value == source)
                    break;

                node = node.Next;
            }

            if (node == null)
                throw new InvalidOperationException("Failed to find handler " + node.Value.GetType().FullName + " in the ppeline.");

            var context = source.HandlerContexts.Get(node.Value);
            node.Value.HandleDownstream(context, channelEvent);
            */
        }
    }
}