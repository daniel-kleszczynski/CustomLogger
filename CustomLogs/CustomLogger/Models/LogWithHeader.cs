namespace CustomLogs.Models
{
    public abstract class LogWithHeader
    {
        internal LogWithHeader(string path, string callerName, int callerLine)
        {
            Path = path;
            CallerName = callerName;
            CallerLine = callerLine;
        }

        internal string Path { get; private set; }
        internal string CallerName { get; private set; }
        internal int CallerLine { get; private set; }
    }
}
