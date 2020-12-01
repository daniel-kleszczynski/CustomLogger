using System;

namespace CustomLogs.Sinks
{
    public interface ISink : IDisposable
    {
        void Enable(string programName, string userName, int delayMs);
        void Flush();
        void Log(string message, string path, string callerName, int callerLine);
    }
}
