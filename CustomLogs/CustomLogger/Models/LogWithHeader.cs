using CustomLogs.Enums;
using System;

namespace CustomLogs.Models
{
    public abstract class LogWithHeader
    {
        internal LogWithHeader(string path, string callerName, int callerLine, string userName)
        {
            Path = path;
            CallerName = callerName;
            CallerLine = callerLine;
            UserName = userName;
            DateTime = DateTime.Now;
        }

        internal LogStatus LogStatus { get; private set; }
        internal string Path { get; private set; }
        internal string CallerName { get; private set; }
        internal int CallerLine { get; private set; }
        internal string UserName { get; private set; }
        internal DateTime DateTime { get; private set; }
    }
}
