using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class SystemConcurrencyException:Exception
    {
       public SystemConcurrencyException(string message) : base (message)
        {

        }

        public SystemConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
