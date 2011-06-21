using System.IO;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Pipelines;

namespace Griffin.Core.Net.Channels
{
    /// <summary>
    /// I/O Channel
    /// </summary>
    /// <remarks>
    /// <para>
    /// Channels are used to transport everything to/from the remote end point.
    /// </para>
    /// <para>
    /// The channel implements upstream handler and downstream handler to be able to know when
    /// a pipeline is done. The channel is automatically assigned as the last handler in both directions.
    /// For the upstream, it's to know when a next read can be initated and for downstream
    /// to be able to send the buffer.
    /// </para>
    /// <para>
    /// The channel expects sends out a <see cref="Stream"/> to the first handler in the upstream
    /// and expects a <c>byte[]</c> from the last downstream handler.
    /// </para>
    /// </remarks>
    public interface IChannel : IDownstreamHandler, IUpstreamHandler
    {
        /// <summary>
        /// Gets pipeline that this channel is attached to.
        /// </summary>
        IPipeline Pipeline { get; }

        /// <summary>
        /// Gets contexts used for each handler in a channel.
        /// </summary>
        IContextCollection HandlerContexts { get; }
    }
}