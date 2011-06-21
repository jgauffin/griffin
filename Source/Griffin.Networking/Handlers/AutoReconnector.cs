using System;
using System.Threading;
using Griffin.Core.Net.Messages;

namespace Griffin.Core.Net.Handlers
{
    /// <summary>
    /// Will try to reconnect automatically if getting disconnected.
    /// </summary>
    /// <remarks>
    /// This event is only valid for client channels.
    /// </remarks>
    public class AutoReconnector : IUpstreamHandler, IDownstreamHandler
    {
        private readonly TimeSpan _interval;
        private Timer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoReconnector"/> class.
        /// </summary>
        /// <param name="interval">Number of seconds to wait between each attempt.</param>
        public AutoReconnector(TimeSpan interval)
        {
            _interval = interval;
        }

        private void OnTryReconnect(object state)
        {
            var ctx = (IChannelHandlerContext) state;
            ctx.SendDownstream(new ConnectEvent());
        }

        #region IDownstreamHandler Members

        /// <summary>
        /// Handle the data that is going to be sent to the remote end point
        /// </summary>
        /// <param name="ctx">Context information</param>
        /// <param name="e">Chennel event.</param>
        public void HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            if (e is CloseEvent && _timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        #endregion

        #region IUpstreamHandler Members

        /// <summary>
        /// Gets if this pipeline can be shared between multiple channels
        /// </summary>
        /// <remarks>
        /// Return <c>false</c> if you have member variables.
        /// </remarks>
        public bool IsSharable
        {
            get { return false; }
        }

        /// <summary>
        /// Process data that was received from the channel.
        /// </summary>
        /// <param name="ctx">Context which is used for the currently processed channel</param>
        /// <param name="e">Event being processed.</param>
        public void HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            if (e is ClosedEvent)
            {
                _timer = new Timer(OnTryReconnect, ctx, _interval, TimeSpan.Zero);
                ctx.SendUpstream(e);
            }
            else if (e is ConnectedEvent)
            {
                _timer.Dispose();
                _timer = null;
                ctx.SendUpstream(e);
            }
        }

        #endregion
    }
}