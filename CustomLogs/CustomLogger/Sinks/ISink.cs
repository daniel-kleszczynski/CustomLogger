using System;

namespace CustomLogs.Sinks
{
    public interface ISink : IDisposable
    {
        void Setup(string programName, string userName, int delayMs);
        void Flush();
        void Log(string message, string path, string callerName, int callerLine);
        void LogData(string paramName, object paramValue, string path, string callerName, int callerLine);
    }
}
