﻿namespace CustomLogs.Models
{
    internal class LogInfo : LogWithHeader
    {
        internal LogInfo(string message, string path, string callerName, int callerLine, string userName)
            : base(path, callerName, callerLine, userName)
        {
            Message = message;
        }

        internal string Message { get; private set; }
    }
}
