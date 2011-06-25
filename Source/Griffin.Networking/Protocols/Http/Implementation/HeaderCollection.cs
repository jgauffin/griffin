using System;
using System.Collections;
using System.Collections.Generic;

namespace Griffin.Networking.Protocols.Http.Implementation
{
    /// <summary>
    /// Collection of headers.
    /// </summary>
    public class HeaderCollection : IHeaderCollection
    {
        private readonly Dictionary<string, string> _headers =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        #region IHeaderCollection Members

        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="name">header name.</param>
        /// <returns>header if found; otherwise <c>null</c>.</returns>
        public string this[string name]
        {
            get { return Get(name); }
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
            Add(name, value);
        }

        /// <summary>
        /// Get a header 
        /// </summary>
        /// <param name="name">Name of header</param>
        /// <returns>Header if found and casted properly; otherwise <c>null</c>.</returns>
        public string Get(string name)
        {
            string value;
            return _headers.TryGetValue(name, out value) ? value : null;
        }

        public void Clear()
        {
            _headers.Clear();
        }
    }
}