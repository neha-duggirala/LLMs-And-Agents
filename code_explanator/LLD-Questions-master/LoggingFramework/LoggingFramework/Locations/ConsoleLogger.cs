using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingFramework.Contract;

namespace LoggingFramework.Locations
{
    public class ConsoleLogger : ILogObserver
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"Writing to Console: {message}");
        }
    }
}
