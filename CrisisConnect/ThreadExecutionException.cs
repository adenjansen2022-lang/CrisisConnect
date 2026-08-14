using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class ThreadExecutionException : Exception
    {
        public ThreadExecutionException(string message) : base(message)
        {
         
        }

        public ThreadExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
