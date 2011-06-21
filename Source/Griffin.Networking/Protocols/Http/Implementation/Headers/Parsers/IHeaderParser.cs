﻿using System;
using Griffin.Core.Net.Buffers;

namespace Griffin.Core.Net.Protocols.Http.Implementation.Headers.Parsers
{
    /// <summary>
    /// Used to parse header values
    /// </summary>
    public interface IHeaderParser
    {
        /// <summary>
        /// Parse a header
        /// </summary>
        /// <param name="name">Name of header.</param>
        /// <param name="reader">Reader containing value.</param>
        /// <returns>HTTP Header</returns>
        /// <exception cref="FormatException">Header value is not of the expected format.</exception>
        IHeader Parse(string name, ITextParser reader);
    }
}