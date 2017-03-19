using System;

namespace ServiceLibrary
{
    public class EmptyLastNameException : Exception
    {
        public EmptyLastNameException()
        {
        }

        public EmptyLastNameException(string message) : base(message)
        {
        }

        public EmptyLastNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
