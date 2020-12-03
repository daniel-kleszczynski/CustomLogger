using CustomLogs.Models;
using System;

namespace CustomLogs.Sinks
{
    public abstract class Sink : IDisposable
    {
        public abstract void Dispose();
        internal abstract void Setup(string programName, string userName, int delayMs);
        internal abstract void Flush();
        internal abstract void Log(LogInfo logModel);
        internal abstract void LogData(LogDataInfo logModel);
        internal abstract void LogDataSet(LogDataSetInfo logModel);
        internal abstract void LogCollection<TItem>(LogCollectionInfo<TItem> logModel);
        internal abstract void LogException<TException>(LogExceptionInfo<TException> logModel) where TException : Exception;
        internal abstract void LogError(LogInfo logModel);
    }
}
