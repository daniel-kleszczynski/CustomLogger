using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogs
{
    public interface ICustomLogger
    {
        void Start();
        void Log(string message, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1);
    }
}
