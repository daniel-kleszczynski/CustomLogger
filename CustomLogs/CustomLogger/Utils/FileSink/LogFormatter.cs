using CustomLogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogs.Utils.FileSink
{
    internal interface ILogFormatter
    {
        string FormatHeader(LogWithHeader logModel);
        string FormatLogData(LogDataInfo logModel);
    }

    internal class LogFormatter : ILogFormatter
    {
        private const string StatusOk = "[OK]";

        public string FormatHeader(LogWithHeader logModel)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            var inCodeLocation = $"{{  + {logModel.CallerName} + (linia: {logModel.CallerLine}) }})";
            return $"{StatusOk} {time} {logModel.Path} {inCodeLocation}";
        }

        public string FormatLogData(LogDataInfo logModel)
        {
            var content = "     Data: ";

            var value = logModel.ParamValue != null ? logModel.ParamValue : "NULL";
            value = value.Equals(string.Empty) ? "\"\"" : value;
            content += $"[{logModel.ParamName}] = {value} ";

            return content;
        }
    }
}
