using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
