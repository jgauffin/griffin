using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Core.Net.Buffers;

namespace Griffin.Core.Net.Protocols.Http.Implementation
{
    class QueryStringDecoder
    {
        public static ParameterCollection Parse(ITextParser reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var parameters = new ParameterCollection();
            while (!reader.EndOfFile)
            {
                string name = Uri.UnescapeDataString(reader.ReadToEnd("&="));
                char current = reader.Current;
                reader.Consume();
                switch (current)
                {
                    case '&':
                        parameters.Add(name, string.Empty);
                        break;
                    case '=':
                        {
                            string value = reader.ReadToEnd("&");
                            reader.Consume();
                            parameters.Add(name, Uri.UnescapeDataString(value));
                        }
                        break;
                    default:
                        parameters.Add(name, string.Empty);
                        break;
                }
            }

            return parameters;
        }

        /// <summary>
        /// Parse a query string
        /// </summary>
        /// <param name="queryString">string to parse</param>
        /// <returns>A collection</returns>
        /// <exception cref="ArgumentNullException"><c>queryString</c> is <c>null</c>.</exception>
        public static ParameterCollection Parse(string queryString)
        {
            if (queryString == null)
                throw new ArgumentNullException("queryString");
            if (queryString.Length == 0)
                return new ParameterCollection();

            var reader = new StringParser(queryString);
            return Parse(reader);
        }

    }
}
