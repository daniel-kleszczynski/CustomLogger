namespace CustomLogs.Models
{
    internal class LogInfo
    {
        internal LogInfo(string message, string path, string callerName, int callerLine)
        {
            Message = message;
            Path = path;
            CallerName = callerName;
            CallerLine = callerLine;
        }

        internal string Message { get; private set; }
        internal string Path { get; private set; }
        internal string CallerName { get; private set; }
        internal int CallerLine { get; private set; }
    }
}
