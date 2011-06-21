﻿using System;

namespace Griffin.Core.Net.Protocols.Http.Implementation
{
    /// <summary>
    /// cookie sent by the client/browser
    /// </summary>
    /// <seealso cref="ResponseCookie"/>
    public class HttpCookie : IHttpCookie
    {
        private readonly string _name;
        private string _value;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">cookie identifier</param>
        /// <param name="content">cookie content</param>
        /// <exception cref="ArgumentNullException">id or content is null</exception>
        /// <exception cref="ArgumentException">id is empty</exception>
        public HttpCookie(string id, string content)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");
            if (content == null) throw new ArgumentNullException("content");

            _name = id;
            _value = content;
        }

        #region inherited methods

        /// <summary>
        /// Gets the cookie HTML representation.
        /// </summary>
        /// <returns>cookie string</returns>
        public override string ToString()
        {
            return string.Format("{0}={1}; ", Uri.EscapeUriString(_name), Uri.EscapeUriString(_value));
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets the cookie identifier.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }


        /// <summary>
        /// Gets value. 
        /// </summary>
        /// <remarks>
        /// Set to <c>null</c> to remove cookie.
        /// </remarks>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion
    }
}