namespace CustomLogs.Models
{
    public class LogDataSetInfo : LogWithHeader
    {
        public LogDataSetInfo(DataInfo[] dataArray, string callerPath, string callerName, int callerLine,
                              string userName)
            : base(callerPath, callerName, callerLine, userName)
        {
            DataArray = dataArray;
        }

        public DataInfo[] DataArray { get; private set; }
    }
}
