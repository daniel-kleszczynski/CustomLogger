namespace CustomLogs.Models
{
    internal class LogDataInfo
    {
        internal LogDataInfo(string paramName, object paramValue, string path, string callerName, int callerLine)
        {
            ParamName = paramName;
            ParamValue = paramValue;
            Path = path;
            CallerName = callerName;
            CallerLine = callerLine;
        }

        internal string ParamName { get; private set; }
        internal object ParamValue { get; private set; }
        internal string Path { get; private set; }
        internal string CallerName { get; private set; }
        internal int CallerLine { get; private set; }
    }
}
