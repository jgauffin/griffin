using System;
using System.IO;
using System.Text;
using Griffin.Core.Net.Buffers;
using Griffin.Core.Net.Protocols.Http.Implementation.Headers;
using Griffin.Core.Net.Protocols.Http.Implementation.Headers.Parsers;

namespace Griffin.Core.Net.Protocols.Http.Implementation
{
    /// <summary>
    /// Request implementation.
    /// </summary>
    internal class HttpRequest : HttpMessage, IRequest
    {
        private HttpCookieCollection _cookies;
        private IParameterCollection _form;
        private MemoryStream _memoryStream = new MemoryStream();

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequest"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="path">The path.</param>
        /// <param name="version">The version.</param>
        public HttpRequest()
        {
            Body = _memoryStream;
        	ContentEncoding = Encoding.UTF8;
        }


        /// <summary>
        /// Gets request URI.
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Gets cookies.
        /// </summary>
        public IHttpCookieCollection<IHttpCookie> Cookies
        {
            get { return _cookies ?? (_cookies = new HttpCookieCollection()); }
        }

        /// <summary>
        /// Gets all uploaded files.
        /// </summary>
        public IHttpFileCollection Files { get; internal set; }

        /// <summary>
        /// Gets form parameters.
        /// </summary>
        public IParameterCollection Form
        {
            get { return _form; }
            internal set
            {
                _form = value;
            }
        }

        /// <summary>
        /// Gets query string.
        /// </summary>
        public IParameterCollection QueryString { get; internal set; }

        /// <summary>
        /// Gets if request is an Ajax request.
        /// </summary>
        public bool IsAjax
        {
            get
            {
                return Headers["X-Requested-With"].Contains("XMLHttpRequest");
            }
        }

        /// <summary>
        /// Gets or sets HTTP method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets requested URI.
        /// </summary>
        Uri IRequest.Uri
        {
            get { return Uri; }
            set { Uri = value; }
        }

        public override void Add(string name, string value)
        {
            string lowerName = name.ToLower();
            if (lowerName == "host")
            {
                string method = HttpContext.Current.IsSecure ? "https://" : "http://";
                Uri = new Uri(method + value + Uri.PathAndQuery);
                return;
            }
            if (lowerName == "content-length")
            {
                ContentLength = int.Parse(value);
                if (ContentLength > 5000000)
                {
                    Body = new FileStream(Path.GetTempFileName(), FileMode.CreateNew, FileAccess.ReadWrite,
                                          FileShare.Read);
                }
            }
            if (lowerName == "content-type")
            {
                var parser = new ContentTypeParser();
                var header = (ContentTypeHeader) parser.Parse("fejk", new Buffers.StringParser(value));
                ContentType = header.Value;
                string charset = header.Parameters["charset"];
                if (!string.IsNullOrEmpty(charset))
                    ContentEncoding = Encoding.GetEncoding(charset);
            }
            if (lowerName == "cookie")
            {
                _cookies = new HttpCookieCollection(value);
            }

            base.Add(name, value);
        }

        public override void SetFirstLine(string method, string path, string version)
        {
            Method = method;
            ProtocolVersion = version;
            // Parse query string.
            int pos = path.IndexOf("?");
            QueryString = pos != -1 ? QueryStringDecoder.Parse(path.Substring(pos + 1)) : new ParameterCollection();
            Uri = new Uri("http://not.specified.yet" + path);
        }
    }
}