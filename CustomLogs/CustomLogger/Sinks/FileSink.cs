using CustomLogs.Models;
using CustomLogs.Utils.FileSink;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace CustomLogs.Sinks
{
    public class FileSink : Sink
    {
        private const string StatusOk = "[OK]";

        private readonly string _rootDirectory;
        private string _filePath;

        private readonly IFilePathBuilder _filePathBuilder;
        private readonly IAsyncFileWriter _fileWriter;

        private ConcurrentQueue<string[]> _logQueue { get; } = new ConcurrentQueue<string[]>();

        internal FileSink(string rootDirectory, IFilePathBuilder filePathBuilder, IAsyncFileWriter fileWriter)
        {
            _rootDirectory = rootDirectory;
            _filePathBuilder = filePathBuilder;
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
            var time = DateTime.Now.ToString("HH:mm:ss");
            var inCodeLocation = $"{{  + {logModel.CallerName} + (linia: {logModel.CallerLine}) }})";
            var header = $"{StatusOk} {time} {logModel.Path} {inCodeLocation}";

            _logQueue.Enqueue(new string[] { header, logModel.Message });
        }

        internal override void LogData(LogDataInfo logModel)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            var inCodeLocation = $"{{  + {logModel.CallerName} + (linia: {logModel.CallerLine}) }})";
            var header = $"{StatusOk} {time} {logModel.Path} {inCodeLocation}";
            var content = "     Data: ";

            var value = logModel.ParamValue != null ? logModel.ParamValue : "NULL";
            value = value.Equals(string.Empty) ? "\"\"" : value;
            content += $"[{logModel.ParamName}] = {value} ";

            _logQueue.Enqueue(new string[] { header, content });
        }
    }
}
