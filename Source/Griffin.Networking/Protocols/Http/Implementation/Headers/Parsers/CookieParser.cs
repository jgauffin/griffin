using System;
using Griffin.Networking.Buffers;

namespace Griffin.Networking.Protocols.Http.Implementation.Headers.Parsers
{
    /// <summary>
    /// Parses Cookie header.
    /// </summary>
    [ParserFor(CookieHeader.NAME)]
    public class CookieParser : IHeaderParser
    {
        #region IHeaderParser Members

        /// <summary>
        /// Parse a header
        /// </summary>
        /// <param name="name">Name of header.</param>
        /// <param name="reader">Reader containing value.</param>
        /// <returns>HTTP Header</returns>
        /// <exception cref="FormatException">Header value is not of the expected format.</exception>
        public IHeader Parse(string name, ITextParser reader)
        {
            //key: "value"; key: "value"

            var cookies = new HttpCookieCollection();
            while (!reader.EndOfFile)
            {
                // read name
                string cookieName = reader.ReadToEnd("=;");

                // cookie with value?
                if (reader.Current == '=')
                {
                    reader.Consume();
                    reader.ConsumeWhiteSpaces();

                    // is value quoted or not?
                    string value = reader.Current == '"' ? reader.ReadQuotedString() : reader.ReadToEnd(";");
                    cookies.Add(new HttpCookie(cookieName, value));
                }
                //else
                //    cookies.Add(new RequestCookie(cookieName, string.Empty));

                // consume whitespaces and cookie separator
                reader.ConsumeWhiteSpaces(';');
            }

            return new CookieHeader(cookies);
        }

        #endregion
    }
}