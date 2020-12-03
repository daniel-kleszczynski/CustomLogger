namespace CustomLogs.Models
{
    public class LogDataSetInfo : LogWithHeader
    {
        public LogDataSetInfo(DataInfo[] dataArray, string callerPath, string callerName, int callerLine)
            : base(callerPath, callerName, callerLine)
        {
            DataArray = dataArray;
        }

        public DataInfo[] DataArray { get; private set; }
    }
}
