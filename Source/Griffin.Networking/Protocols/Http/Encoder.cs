using System;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;

namespace Griffin.Networking.Protocols.Http
{
    public class Encoder : IDownstreamHandler
    {
        /// <summary>
        /// Gets if this pipeline can be shared between multiple channels
        /// </summary>
        /// <remarks>
        /// Return <c>false</c> if you have member variables.
        /// </remarks>
        public bool IsSharable
        {
            get { throw new NotImplementedException(); }
        }

        #region IDownstreamHandler Members

        /// <summary>
        /// Handle the data that is going to be sent to the remote end point
        /// </summary>
        /// <param name="ctx">Context information</param>
        /// <param name="e">Chennel event.</param>
        public void HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}