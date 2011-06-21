namespace Griffin.Core.Net.Protocols.Http.Implementation.Headers
{
    /// <summary>
    /// Type of HTTP connection
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        /// Connection is closed after each request-response
        /// </summary>
        Close,

        /// <summary>
        /// Connection is kept alive for X seconds (unless another request have been made)
        /// </summary>
        KeepAlive
    }
}