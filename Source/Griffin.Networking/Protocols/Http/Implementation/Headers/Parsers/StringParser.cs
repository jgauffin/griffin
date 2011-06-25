using System;
using Griffin.Networking.Buffers;

namespace Griffin.Networking.Protocols.Http.Implementation.Headers.Parsers
{
    internal class StringParser : IHeaderParser
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
            return null;
        }

        #endregion
    }
}