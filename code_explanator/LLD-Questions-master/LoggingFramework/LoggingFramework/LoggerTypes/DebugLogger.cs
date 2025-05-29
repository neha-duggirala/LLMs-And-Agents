using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingFramework.Contract;
using LoggingFramework.Locations;

namespace LoggingFramework.LoggerTypes
{
    public class DebugLogger : AbstractLogger
    {
        public DebugLogger(LogLevel logLevel)
        {
            this.LogLevel = logLevel;
        }
        protected override void Display(string message, LoggerTarget loggerTarget)
        {
            loggerTarget.NotifyAllObserver(this.LogLevel,"Debug : "+message);
        }
    }
}
