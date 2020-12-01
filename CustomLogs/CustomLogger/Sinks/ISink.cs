using System;
using System.Collections.Concurrent;

namespace CustomLogs.Sinks
{
    public interface ISink : IDisposable
    {
        ConcurrentQueue<string[]> LogQueue { get; }
        void Enable(string programName, string userName, int delayMs);
        void Flush();

        void Log(string message, string path, string callerName, int callerLine);
    }
}
