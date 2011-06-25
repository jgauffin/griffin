using System.IO;
using System.Text;

namespace Griffin.Networking.Protocols.Http.Implementation
{
    internal abstract class HttpMessage : IMessage
    {
        private readonly HeaderCollection _headers = new HeaderCollection();

        public HttpMessage()
        {
            _headers = new HeaderCollection();
        }

        /// <summary>
        /// Gets connection type.
        /// </summary>
        public bool KeepAlive
        {
            get { return Headers["Connection"].Contains("Keep-Alive"); }
            set { Headers["Connection"] = value ? "Keep-Alive" : "Close"; }
        }

        /// <summary>
        /// Kind of content in the body
        /// </summary>
        /// <remarks>Default is <c>text/html</c></remarks>
        public string ContentType { get; set; }

        #region IMessage Members

        /// <summary>
        /// Gets current protocol version
        /// </summary>
        /// <value>
        /// Default is HTTP/1.1
        /// </value>
        public string ProtocolVersion { get; protected set; }

        /// <summary>
        /// Gets or sets body stream.
        /// </summary>
        public Stream Body { get; set; }

        /// <summary>
        /// Gets number of bytes in the content
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        /// Gets or sets encoding
        /// </summary>
        public Encoding ContentEncoding { get; set; }

        /// <summary>
        /// Gets headers.
        /// </summary>
        public IHeaderCollection Headers
        {
            get { return _headers; }
        }

        #endregion

        /// <summary>
        /// Add a new header.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public virtual void Add(string name, string value)
        {
            _headers.Add(name, value);
        }

        /// <summary>
        /// Reset message so that it can be reused for the next request/reply
        /// </summary>
        public virtual void Reset()
        {
            _headers.Clear();
            ContentEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// Used to assign first line
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <param name="word3"></param>
        /// <remarks>
        /// The meaning of the arguments differs depending on if it's a request or a reply.
        /// </remarks>
        /// <seealso cref="HttpRequest.SetFirstLine"/>
        /// <seealso cref="HttpResponse.SetFirstLine"/>
        public abstract void SetFirstLine(string word1, string word2, string word3);
    }
}