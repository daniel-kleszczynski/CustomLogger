namespace CustomLogs.Models
{
    internal class LogInfo : LogWithHeader
    {
        internal LogInfo(string message, string path, string callerName, int callerLine)
            : base(path, callerName, callerLine)
        {
            Message = message;
        }

        internal string Message { get; private set; }
    }
}
