using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Griffin.Core.Net.Protocols.Http.Implementation.Headers;
using HttpServer;

namespace Griffin.Core.Net.Protocols.Http.Implementation
{
    class HttpResponse : HttpMessage, IResponse
    {
        public HttpResponse()
        {
        }


        /// <summary>
        /// Gets cookies.
        /// </summary>
        public IHttpCookieCollection<IResponseCookie> Cookies { get; private set; }

        /// <summary>
        /// Information about why a specific status code was used.
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Status code that is sent to the client.
        /// </summary>
        /// <remarks>Default is <see cref="HttpStatusCode.OK"/></remarks>
        public int StatusCode { get; set; }

        ///<summary>
        /// Gets or sets content type
        ///</summary>
        public ContentTypeHeader ContentType { get; set; }

        /// <summary>
        /// Redirect user.
        /// </summary>
        /// <param name="uri">Where to redirect to.</param>
        /// <remarks>
        /// Any modifications after a redirect will be ignored.
        /// </remarks>
        public void Redirect(string uri)
        {
            throw new NotImplementedException();
        }

        public override void SetFirstLine(string version, string codeStr, string reason)
        {
            int code;
            if (!int.TryParse(codeStr, out code))
                throw new BadRequestException("Invalid status code {0} on response line.".FormatWith(codeStr));

            ProtocolVersion = version;
            StatusCode = code;
            StatusDescription = reason;
        }
    }
}
