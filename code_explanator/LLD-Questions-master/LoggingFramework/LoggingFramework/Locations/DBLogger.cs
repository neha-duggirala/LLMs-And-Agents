using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingFramework.Contract;

namespace LoggingFramework.Locations
{
    public class DBLogger : ILogObserver
    {
        public void WriteMessage(string message)
        {
            // Simulate writing to a database
            Console.WriteLine($"Writing to DB: {message}");
        }
    }
}
