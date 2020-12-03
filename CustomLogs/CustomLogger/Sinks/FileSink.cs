using CustomLogs.Models;
using CustomLogs.Enums;
using CustomLogs.Utils.FileSink;
using System.Collections.Concurrent;
using System.IO;
using System;
using System.Linq;

namespace CustomLogs.Sinks
{
    public class FileSink : Sink
    {
        private readonly string _rootDirectory;
        private string _filePath;

        private readonly IFilePathBuilder _filePathBuilder;
        private readonly ILogFormatter _logFormatter;
        private readonly IAsyncFileWriter _fileWriter;

        private ConcurrentQueue<string[]> _logQueue { get; } = new ConcurrentQueue<string[]>();

        internal FileSink(string rootDirectory, 
                          IFilePathBuilder filePathBuilder,
                          ILogFormatter logFormatter,
                          IAsyncFileWriter fileWriter)
        {
            _rootDirectory = rootDirectory;
            _filePathBuilder = filePathBuilder;
            _logFormatter = logFormatter;
            _fileWriter = fileWriter;
        }

        public override void Dispose()
        {
            Flush();
        }

        internal override void Setup(string programName, string userName, int delayMs)
        {
            try
            {
                if (!Directory.Exists(_rootDirectory))
                    return;

                var directoryPath = Path.Combine(_rootDirectory, programName);
                _filePath = _filePathBuilder.Build(directoryPath, programName, userName);

                Directory.CreateDirectory(directoryPath);
            }
            finally { }

            _fileWriter.Start(_logQueue, _filePath, delayMs);
        }

        internal override void Flush()
        {
            if (Directory.Exists(_rootDirectory))
                _fileWriter.WriteLog(_logQueue, _filePath);
        }

        internal override void Log(LogInfo logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.OK);
            _logQueue.Enqueue(new string[] { header, logModel.Message });
        }

        internal override void LogData(LogDataInfo logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.OK);
            var content = _logFormatter.FormatLogData(logModel);

            _logQueue.Enqueue(new string[] { header, content });
        }

        internal override void LogDataSet(LogDataSetInfo logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.OK);
            var content = _logFormatter.FormatLogDataSet(logModel);

            _logQueue.Enqueue(new string[] { header, content });
        }

        internal override void LogCollection<TItem>(LogCollectionInfo<TItem> logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.OK);
            var content = _logFormatter.FormatLogCollection(logModel);

            _logQueue.Enqueue(new string[] { header, content });
        }

        internal override void LogException<TException>(LogExceptionInfo<TException> logModel)
        {
            var header = _logFormatter.FormatExceptionHeader(logModel);
            var contentArray = _logFormatter.FormatLogException(logModel);
            var combineArray = (new string[] { header }).Concat(contentArray).ToArray();

            _logQueue.Enqueue(combineArray);
        }
    }
}
