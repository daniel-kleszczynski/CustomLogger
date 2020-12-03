using CustomLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerDemo
{
    public class LogPresenter
    {
        public void SimpleLogDemo(ICustomLogger logger)
        {
            logger.Log("Simple log example");
        }

        public void LogDataDemo(ICustomLogger logger)
        {
            int x = 7;
            logger.LogData(nameof(x), x);
        }
    }
}
