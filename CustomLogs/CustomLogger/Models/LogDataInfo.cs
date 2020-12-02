namespace CustomLogs.Models
{
    public class LogDataInfo
    {
        public LogDataInfo(string paramName, object paramValue, string path, string callerName, int callerLine)
        {
            ParamName = paramName;
            ParamValue = paramValue;
            Path = path;
            CallerName = callerName;
            CallerLine = callerLine;
        }

        public string ParamName { get; private set; }
        public object ParamValue { get; private set; }
        public string Path { get; private set; }
        public string CallerName { get; private set; }
        public int CallerLine { get; private set; }
    }
}
