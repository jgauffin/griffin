﻿using System.Collections;
using System.Collections.Generic;

namespace Griffin.Networking.Protocols.Http.Implementation
{
    /// <summary>
    /// A parameter in <see cref="IParameterCollection"/>.
    /// </summary>
    public class Parameter : IParameter
    {
        private readonly List<string> _values = new List<string>();

        public Parameter(string name, params string[] values)
        {
            Name = name;
            _values.AddRange(values);
        }

        #region IParameter Members

        /// <summary>
        /// Gets last value.
        /// </summary>
        /// <remarks>
        /// Parameters can have multiple values. This property will always get the last value in the list.
        /// </remarks>
        /// <value>String if any value exist; otherwise <c>null</c>.</value>
        public string Value
        {
            get { return _values.Count == 0 ? null : _values[_values.Count - 1]; }
        }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a list of all values.
        /// </summary>
        public IEnumerable<string> Values
        {
            get { return _values; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _values.GetEnumerator();
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
            return _values.GetEnumerator();
        }

        #endregion

        public void Add(string value)
        {
            _values.Add(value);
        }
    }
}