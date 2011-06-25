using System;
using System.Collections;
using System.Collections.Generic;

namespace Griffin.Networking.Protocols.Http.Implementation.Headers
{
    /// <summary>
    /// Collection of headers.
    /// </summary>
    public class HeaderCollection : IHeaderCollection
    {
        private readonly HeaderFactory _factory;

        private readonly Dictionary<string, string> _headers =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCollection"/> class.
        /// </summary>
        /// <param name="factory">Factory used to created headers.</param>
        public HeaderCollection(HeaderFactory factory)
        {
            _factory = factory;
        }

        #region IHeaderCollection Members

        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="name">header name.</param>
        /// <returns>header if found; otherwise <c>null</c>.</returns>
        public string this[string name]
        {
            get
            {
                string header;
                return _headers.TryGetValue(name, out header) ? header : null;
            }
            set
            {
                if (value == null)
                    _headers.Remove(name);
                else
                    _headers[name] = value;
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _headers.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Add a header.
        /// </summary>
        /// <param name="name">Header name</param>
        /// <param name="value">Header value</param>
        /// <remarks>
        /// Will try to parse the header and create a <see cref="IHeader"/> object.
        /// </remarks>
        /// <exception cref="FormatException">Header value is not correctly formatted.</exception>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>value</c> is <c>null</c>.</exception>
        public void Add(string name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");
            IHeader header = _factory.Parse(name, value);
            if (header == null)
                throw new FormatException("Failed to parse header " + name + "/" + value + ".");
            _headers[name] = value;
        }
    }
}