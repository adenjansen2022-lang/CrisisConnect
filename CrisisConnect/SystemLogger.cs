using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrisisConnect
{
    internal class SystemLogger
    {
        private readonly object logLock = new object();
        private readonly string logFilePath;

        public SystemLogger()
        {
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CrisisConnect.log");

        }

        public void Log(string message)
        {
            lock (logLock)
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

                File.AppendAllText(logFilePath, logEntry + Environment.NewLine); 
            }
        }
    }
}
