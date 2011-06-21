using System.Collections.Generic;
using Griffin.Core.Net.Handlers;

namespace Griffin.Core.Net.Pipelines
{
    /// <summary>
    /// A pipeline is used to process data when it's going from the channel to your application and vice versa.
    /// </summary>
    /// <remarks>
    /// By using a pipe line we can let a arbitrary number of reusable handlers process the data without
    /// having to change a single line of code.
    /// </remarks>
    public interface IPipeline
    {
        IEnumerable<IDownstreamHandler> DownstreamHandlers { get; }

        IEnumerable<IUpstreamHandler> UpstreamHandlers { get; }

        /// <summary>
        /// Register a downstrewam handler (from your application down to the channel).
        /// </summary>
        /// <param name="handler">The channel handler</param>
        /// <remarks>
        /// All handlers should be registered in the order that they are going
        /// to process the data. (the last handler is the last that will process
        /// the data before the channel sends it out).
        /// </remarks>
        void RegisterDownstreamHandler(IDownstreamHandler handler);

        /// <summary>
        /// Register a handler for the upstream (from the channel up to your application).
        /// </summary>
        /// <param name="handler">A channel handler</param>
        /// <remarks>
        /// All handlers should be registered in the order that they are going to process
        /// the data. Your application should be the last registered handler.
        /// </remarks>
        void RegisterUpstreamHandler(IUpstreamHandler handler);
    }
}