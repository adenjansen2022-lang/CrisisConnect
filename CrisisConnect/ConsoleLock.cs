using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class ConsoleLock
    {
         public static readonly object LockObject = new object();
    }
}
