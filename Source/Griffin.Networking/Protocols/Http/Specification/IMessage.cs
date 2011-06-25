﻿using System.IO;
using System.Text;

namespace Griffin.Networking.Protocols.Http
{
    /// <summary>
    /// Base interface for request and response.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets current protocol version
        /// </summary>
        /// <value>
        /// Default is HTTP/1.1
        /// </value>
        string ProtocolVersion { get; }

        /// <summary>
        /// Gets or sets body stream.
        /// </summary>
        Stream Body { get; }

        /// <summary>
        /// Gets number of bytes in the body
        /// </summary>
        int ContentLength { get; }

        /// <summary>
        /// Gets or sets content encoding
        /// </summary>
        Encoding ContentEncoding { get; set; }

        /// <summary>
        /// Gets headers.
        /// </summary>
        IHeaderCollection Headers { get; }
    }
}