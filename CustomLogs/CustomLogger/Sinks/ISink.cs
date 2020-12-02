using CustomLogs.Models;
using System;

namespace CustomLogs.Sinks
{
    public interface ISink : IDisposable
    {
        void Setup(string programName, string userName, int delayMs);
        void Flush();
        void Log(LogInfo logModel);
        void LogData(LogDataInfo logModel);
    }
}
