using System;

namespace Griffin.Networking
{
    internal class BadRequestException : Exception
    {
        public BadRequestException(string errorMessage)
        {
            throw new NotImplementedException();
        }

        public BadRequestException(string errorMessage, Exception innerException)
        {
        }
    }
}