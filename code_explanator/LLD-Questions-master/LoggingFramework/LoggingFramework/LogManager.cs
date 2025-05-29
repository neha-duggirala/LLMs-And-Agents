using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingFramework.Contract;
using LoggingFramework.Locations;
using LoggingFramework.LoggerTypes;

namespace LoggingFramework
{
    public class LogManager
    {

        public static AbstractLogger DoChaining()
        {
            AbstractLogger Info = new InfoLogger(LogLevel.INFO);
            AbstractLogger Debug = new DebugLogger(LogLevel.DEBUG);
            AbstractLogger Error = new ErrorLogger(LogLevel.ERROR);
            Info.NextLogger = Debug;
            Debug.NextLogger = Error;
            return Info;
        }
        public static LoggerTarget AddObservers()
        {
            LoggerTarget loggerTarget = new LoggerTarget();

            FileLogger fileLogger = new FileLogger();
            ConsoleLogger consoleLogger = new ConsoleLogger();  
            DBLogger dBLogger = new DBLogger();

            loggerTarget.AddObserver(LogLevel.INFO, fileLogger);
            loggerTarget.AddObserver(LogLevel.INFO, consoleLogger);
            loggerTarget.AddObserver(LogLevel.INFO, dBLogger);

            loggerTarget.AddObserver(LogLevel.DEBUG, fileLogger);
            loggerTarget.AddObserver(LogLevel.DEBUG, consoleLogger);

            loggerTarget.AddObserver(LogLevel.ERROR, fileLogger);

            return loggerTarget;
        }
    }
}
