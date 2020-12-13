namespace CustomLogs.Models
{
    internal class LogDataInfo : LogWithHeader
    {
        internal LogDataInfo(string paramName, object paramValue, string path, string callerName, int callerLine,
                             string userName)
            : base(path, callerName, callerLine, userName)
        {
            ParamName = paramName;
            ParamValue = paramValue;
        }

        internal string ParamName { get; private set; }
        internal object ParamValue { get; private set; }
    }
}
