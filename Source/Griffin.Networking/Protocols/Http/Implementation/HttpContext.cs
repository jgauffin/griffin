namespace Griffin.Networking.Protocols.Http.Implementation
{
    internal class HttpContext
    {
        private bool _isSecure;

        public static HttpContext Current { get; internal set; }

        public bool IsSecure
        {
            get { return _isSecure; }
        }
    }
}