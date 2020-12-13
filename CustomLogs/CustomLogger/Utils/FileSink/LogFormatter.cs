using CustomLogs.Enums;
using CustomLogs.Models;
using System;
using System.Linq;

namespace CustomLogs.Utils.FileSink
{
    internal interface ILogFormatter
    {
        string FormatHeader(LogWithHeader logModel, LogStatus logStatus);
        string FormatExceptionHeader<TException>(LogExceptionInfo<TException> logModel) where TException : Exception;
        string FormatLogCollection<TItem>(LogCollectionInfo<TItem> logModel);
        string FormatLogData(LogDataInfo logModel);
        string FormatLogDataSet(LogDataSetInfo logModel);
        string[] FormatLogException<TException>(LogExceptionInfo<TException> logModel) where TException : Exception;
    }

    internal class LogFormatter : ILogFormatter
    {
        private const string StatusOk = "[OK]";
        private const string StatusError = "[ERROR]";

        public string FormatHeader(LogWithHeader logModel, LogStatus logStatus)
        {
            var time = logModel.DateTime.ToString("HH:mm:ss");
            var fileName = ExtractFileName(logModel.Path);
            var status = logStatus == LogStatus.OK ? StatusOk : StatusError;

            var location = string.IsNullOrWhiteSpace(logModel.UserName) ?
                $"{{file: {fileName}, caller: {logModel.CallerName}, line: {logModel.CallerLine}}}"
            :
                $"{{user: {logModel.UserName}, file: {fileName}, caller: {logModel.CallerName}, line: {logModel.CallerLine}}}";

            return $"{status} {time} {location} in {logModel.Path}";
        }

        public string FormatExceptionHeader<TException>(LogExceptionInfo<TException> logModel) where TException : Exception
        {
            var time =logModel.DateTime.ToString("HH:mm:ss");
            var exceptionType = typeof(TException).Name;
            var catchStatus = logModel.IsCatched ? "CATCHED" : "UNCATCHED";

            return $"{StatusError} {time} ||{catchStatus}|| {exceptionType} : {logModel.Exception.Message}";
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

        public string FormatLogCollection<TItem>(LogCollectionInfo<TItem> logModel)
        {
            int index = 0;
            var content = $"     {logModel.CollectionName}[{logModel.Collection.Count()}]: ";

            foreach (var item in logModel.Collection)
            {
                var propertyData = logModel.Selector(item);

                var label = string.IsNullOrEmpty(propertyData.Name) ? $"[{index}]" : $"[{index}].{propertyData.Name}";
                var value = propertyData.Value != null ? propertyData.Value : "NULL";
                value = value.Equals(string.Empty) ? "\"\"" : value;
                content += $"{label} = {value} ";

                index++;
            }

            return content;
        }

        public string[] FormatLogException<TException>(LogExceptionInfo<TException> logModel) where TException : Exception
        {
            var stackTrace = $"  {logModel.Exception.StackTrace}";

            if (logModel.Exception.InnerException == null)
                return new string[] { stackTrace };
            else
            {
                var innerEx = logModel.Exception.InnerException;
                var innerHeader = $"     >>>  Inner: {innerEx.GetType().Name} : {innerEx.Message}";
                return new string[] { stackTrace, innerHeader };
            }
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
