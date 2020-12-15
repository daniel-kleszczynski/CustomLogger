using CustomLogs.Models;
using CustomLogs.Enums;
using CustomLogs.Utils.FileSink;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

namespace CustomLogs.Sinks
{
    public class FileSink : Sink
    {
        private readonly string _rootDirectory;
        private readonly IFilePathBuilder _filePathBuilder;
        private readonly ILogFormatter _logFormatter;
        private readonly IAsyncFileWriter _fileWriter;

        private ConcurrentQueue<LogQueueItem> _logQueue { get; } = new ConcurrentQueue<LogQueueItem>();

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

        internal override void Setup(string programName, int delayMs)
        {
            if (!Directory.Exists(_rootDirectory))
                return;

            var directoryPath = Path.Combine(_rootDirectory, programName);
            _filePathBuilder.Init(directoryPath, programName);

            Directory.CreateDirectory(directoryPath);

            _fileWriter.Start(_logQueue, _filePathBuilder, delayMs);
        }

        internal override void Flush()
        {
            if (Directory.Exists(_rootDirectory))
                _fileWriter.WriteLog();
        }

        internal override void Log(LogInfo logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.OK);
            var lines = new string[] { header, logModel.Message };

            _logQueue.Enqueue(new LogQueueItem { DateTime = logModel.DateTime, Lines = lines });
        }

        internal override void LogData(LogDataInfo logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.OK);
            var content = _logFormatter.FormatLogData(logModel);
            var lines = new string[] { header, content };

            _logQueue.Enqueue(new LogQueueItem { DateTime = logModel.DateTime, Lines = lines });
        }

        internal override void LogDataSet(LogDataSetInfo logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.OK);
            var content = _logFormatter.FormatLogDataSet(logModel);
            var lines = new string[] { header, content };

            _logQueue.Enqueue(new LogQueueItem { DateTime = logModel.DateTime, Lines = lines });
        }

        internal override void LogCollection<TItem>(LogCollectionInfo<TItem> logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.OK);
            var content = _logFormatter.FormatLogCollection(logModel);
            var lines = new string[] { header, content };

            _logQueue.Enqueue(new LogQueueItem { DateTime = logModel.DateTime, Lines = lines });
        }

        internal override void LogException<TException>(LogExceptionInfo<TException> logModel)
        {
            var header = _logFormatter.FormatExceptionHeader(logModel);
            var contentArray = _logFormatter.FormatLogException(logModel);
            var combinedArray = (new string[] { header }).Concat(contentArray).ToArray();

            _logQueue.Enqueue(new LogQueueItem { DateTime = logModel.DateTime, Lines = combinedArray });
        }

        internal override void LogError(LogInfo logModel)
        {
            var header = _logFormatter.FormatHeader(logModel, LogStatus.ERROR);
            var lines = new string[] { header, logModel.Message };

            _logQueue.Enqueue(new LogQueueItem { DateTime = logModel.DateTime, Lines = lines });
        }
    }
}
