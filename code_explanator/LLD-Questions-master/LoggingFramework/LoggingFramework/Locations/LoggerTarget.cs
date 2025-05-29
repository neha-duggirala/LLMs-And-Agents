using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingFramework.Contract;

namespace LoggingFramework.Locations
{
    public class LoggerTarget
    {
        private readonly Dictionary<LogLevel ,List<ILogObserver>> Listeners = new Dictionary<LogLevel, List<ILogObserver>>();
        public void AddObserver(LogLevel level, ILogObserver observer)
        {
            if(!Listeners.ContainsKey(level))
            {
                Listeners[level] = new List<ILogObserver>();
            }
            Listeners[level].Add(observer);
        }
        public void RemoveObserver(ILogObserver observer) 
        {
            foreach(var value in Listeners.Values)
            {
                value.Remove(observer);
            }
        }
        public void NotifyAllObserver(LogLevel level,string Message)
        {
            if(Listeners.ContainsKey(level))
            {
                foreach (var observer in Listeners[level])
                {
                    observer.WriteMessage(Message);
                }
            }
        }
    }
}
