using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Net
{
    class BadRequestException : Exception
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
