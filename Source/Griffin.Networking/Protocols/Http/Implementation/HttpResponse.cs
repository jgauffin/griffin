using System;
using System.Net;
using Griffin.Core;
using Griffin.Networking.Protocols.Http.Implementation.Headers;

namespace Griffin.Networking.Protocols.Http.Implementation
{
    internal class HttpResponse : HttpMessage, IResponse
    {
        ///<summary>
        /// Gets or sets content type
        ///</summary>
        public ContentTypeHeader ContentType { get; set; }

        #region IResponse Members

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

        #endregion

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