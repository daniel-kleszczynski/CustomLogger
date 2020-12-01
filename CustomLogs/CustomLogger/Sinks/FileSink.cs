using CustomLogs.Utils.FileSink;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace CustomLogs.Sinks
{
    public class FileSink : ISink
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

        public void Dispose()
        {
            Flush();
        }

        public void Setup(string programName, string userName, int delayMs)
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

        public void Flush()
        {
            if (Directory.Exists(_rootDirectory))
                _fileWriter.WriteLog(_logQueue, _filePath);
        }

        public void Log(string message, string path, string callerName, int callerLine)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            var inCodeLocation = $"{{  + {callerName} + (linia: {callerLine}) }})";
            var header = $"{StatusOk} {time} {path} {inCodeLocation}";

            _logQueue.Enqueue(new string[] { header, message });
        }
    }
}
