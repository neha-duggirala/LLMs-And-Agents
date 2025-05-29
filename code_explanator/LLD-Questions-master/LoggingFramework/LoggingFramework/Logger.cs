using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingFramework.Contract;
using LoggingFramework.Locations;

namespace LoggingFramework
{
    public class Logger
    {
        private static Logger logger;
        private static AbstractLogger chainOfLogger;
        private static LoggerTarget loggerTarget;
        private static object _lock = new object();
        public static Logger GetInstance()
        {
            if(logger == null)
            {
                lock(_lock)
                {
                    if(logger == null)
                    {
                        logger = new Logger();
                        chainOfLogger = LogManager.DoChaining();
                        loggerTarget = LogManager.AddObservers();
                    }
                }
            }
            return logger;
        }
        public void Debug(string message) 
        { 
            WriteMessage(message,LogLevel.DEBUG);
        }
        public void Info(string message) 
        {
            WriteMessage(message,LogLevel.INFO);
        }
        public void Error(string message) 
        {
            WriteMessage(message,LogLevel.ERROR);
        }
        private void WriteMessage(string message,LogLevel logLevel)
        {
            chainOfLogger.LogMessage(message,logLevel,loggerTarget);
        }

    }
}
