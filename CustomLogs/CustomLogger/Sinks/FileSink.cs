using CustomLogs.Utils.FileSink;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CustomLogs.Sinks
{
    public class FileSink : ISink
    {
        private const string StatusOk = "[OK]";

        private readonly string _rootDirectory;
        private string _filePath;

        private readonly IFilePathBuilder _filePathBuilder;

        private ConcurrentQueue<string[]> _logQueue { get; } = new ConcurrentQueue<string[]>();

        internal FileSink(string rootDirectory, FilePathBuilder filePathBuilder)
        {
            _rootDirectory = rootDirectory;
            _filePathBuilder = filePathBuilder;
        }

        public void Dispose()
        {
            Flush();
        }

        public void Enable(string programName, string userName, int delayMs)
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

            var task = Task.Run(() => InitializeWritingToFile(delayMs));
        }

        public void Flush()
        {
            if (Directory.Exists(_rootDirectory))
                WriteLog();
        }

        public void Log(string message, string path, string callerName, int callerLine)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            var inCodeLocation = $"{{  + {callerName} + (linia: {callerLine}) }})";
            var header = $"{StatusOk} {time} {path} {inCodeLocation}";

            _logQueue.Enqueue(new string[] { header, message });
        }

        private async Task<int> InitializeWritingToFile(int delayMs)
        {

            while (true)
            {
                try
                {
                    WriteLog();
                    await Task.Delay(delayMs);
                }
                finally { }
            }

        }

        private void WriteLog()
        {
            List<string> totalLines = new List<string>();


            while (_logQueue.Count > 0)
            {
                var result = _logQueue.TryDequeue(out string[] lines);

                if (!result)
                    break;

                totalLines.AddRange(lines);
            }

            if (totalLines.Count == 0)
                return;

            File.AppendAllLines(_filePath, totalLines);
        }
    }
}
