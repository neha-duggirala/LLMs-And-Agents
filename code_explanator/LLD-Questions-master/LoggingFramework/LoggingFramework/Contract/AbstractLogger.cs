using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingFramework.Locations;

namespace LoggingFramework.Contract
{
    public abstract class AbstractLogger
    {
        public LogLevel LogLevel { get; set; }
        public AbstractLogger NextLogger {  get; set; }
        public void LogMessage(string message, LogLevel logLevel,LoggerTarget loggerTarget)
        {
            if(this.LogLevel <= logLevel)
            {
                Display(message, loggerTarget);
            }
            if (NextLogger != null)
            {
                NextLogger.LogMessage(message, logLevel,loggerTarget);
            }
        }
        protected abstract void Display(string message, LoggerTarget loggerTarget);
    }
    public enum LogLevel
    {
        INFO = 3,
        DEBUG = 2,
        ERROR = 1,
    }
}
