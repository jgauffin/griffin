using Griffin.Messaging;

namespace Griffin.Core.Tests.Messaging
{
    public class FindContactsContext : IRequestContext<FindContactsRequest, FindContactsResponse>
    {
        /// <summary>
        /// Stop processing this request.
        /// </summary>
        /// <remarks>
        /// Invoked when the current handle have successfully
        /// handled the request, invoking more handlers
        /// would corrupt the response.
        /// </remarks>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets information about the request being processed.
        /// </summary>
        public FindContactsRequest Request { get; private set; }

        /// <summary>
        /// Gets object to add response information to.
        /// </summary>
        public FindContactsResponse Response { get; private set; }
    }
}