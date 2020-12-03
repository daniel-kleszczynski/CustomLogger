using CustomLogs.Enums;
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
        string FormatHeader(LogWithHeader logModel, LogStatus logStatus);
        string FormatLogData(LogDataInfo logModel);
        string FormatLogDataSet(LogDataSetInfo logModel);
    }

    internal class LogFormatter : ILogFormatter
    {
        private const string StatusOk = "[OK]";
        private const string StatusError = "[ERROR]";

        public string FormatHeader(LogWithHeader logModel, LogStatus logStatus)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            var fileName = ExtractFileName(logModel.Path);
            var status = logStatus == LogStatus.OK ? StatusOk : StatusError;

            return $"{status} {time} {{file: {fileName}, caller: {logModel.CallerName}," +
                    $" line: {logModel.CallerLine}}} in {logModel.Path}";
        }

        public string FormatLogData(LogDataInfo logModel)
        {
            var content = "     Data: ";

            var value = logModel.ParamValue != null ? logModel.ParamValue : "NULL";
            value = value.Equals(string.Empty) ? "\"\"" : value;
            content += $"[{logModel.ParamName}] = {value} ";

            return content;
        }

        public string FormatLogDataSet(LogDataSetInfo logModel)
        {
            var content = "     Data: ";

            foreach (var parameter in logModel.DataArray)
            {
                var value = parameter.Value != null ? parameter.Value : "NULL";
                value = value.Equals(string.Empty) ? "\"\"" : value;
                content += $"[{parameter.Name}] = {value} ";
            }

            return content;
        }

        private string ExtractFileName(string filePath)
        {
            var fileName = filePath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries)
                           .LastOrDefault();

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Cannot extract filename cause filePath is empty.");

            return fileName;
        }
    }
}
