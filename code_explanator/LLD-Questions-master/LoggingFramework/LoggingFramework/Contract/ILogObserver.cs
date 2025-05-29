
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingFramework.Contract
{
    public interface ILogObserver
    {
        public void WriteMessage(string message);
    }
}
