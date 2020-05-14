using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    public class BackingServiceException : Exception
    {
        public BackingServiceException(string message) : base(message)
        {

        }
    }
}
