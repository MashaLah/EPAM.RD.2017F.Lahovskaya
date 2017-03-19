using System;

namespace ServiceLibrary
{
    public class DoesNotExistsException : Exception
    {
        public DoesNotExistsException()
        {
        }

        public DoesNotExistsException(string message) : base(message)
        {
        }

        public DoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
