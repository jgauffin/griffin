using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Net.Protocols.Http.Implementation
{
    class HttpContext
    {
        private bool _isSecure;

        public static HttpContext Current { get; internal set; }

        public bool IsSecure
        {
            get {
                return _isSecure;
            }
        }
    }
}
