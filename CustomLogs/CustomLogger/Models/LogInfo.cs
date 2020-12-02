namespace CustomLogs.Models
{
    public class LogInfo
    {
        public LogInfo(string message, string path, string callerName, int callerLine)
        {
            Message = message;
            Path = path;
            CallerName = callerName;
            CallerLine = callerLine;
        }

        public string Message { get; private set; }
        public string Path { get; private set; }
        public string CallerName { get; private set; }
        public int CallerLine { get; private set; }
    }
}
