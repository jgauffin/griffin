using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Buffers;

namespace Griffin.Core.Net.Protocols.Http.Implementation.Headers.Parsers
{
    class StringParser : IHeaderParser
    {
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
    }
}
