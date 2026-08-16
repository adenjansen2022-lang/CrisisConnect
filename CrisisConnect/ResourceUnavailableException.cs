using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    // Custom domain exception.
    // Used when a rescue team cannot legally be dispatched.
    internal class ResourceUnavailableException : Exception
    {
        public ResourceUnavailableException(string message)
            : base(message)
        {
        }

        public ResourceUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
