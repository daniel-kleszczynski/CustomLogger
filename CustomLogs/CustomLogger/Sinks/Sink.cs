﻿using CustomLogs.Models;
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
    }
}