using CustomLogs.Utils.FileSink;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogs.Sinks
{
    public class FileSink : ISink
    {
        private string _rootDirectory;
        private readonly IDirectoryPathBuilder _directoryPathBuilder;
        private readonly IFilePathBuilder _filePathBuilder;
        private string _filePath;

        internal FileSink(string rootDirectory, 
                          IDirectoryPathBuilder directoryPathBuilder,
                          IFilePathBuilder filePathBuilder)
        {
            _rootDirectory = rootDirectory;
            _directoryPathBuilder = directoryPathBuilder;
            _filePathBuilder = filePathBuilder;
        }

        public ConcurrentQueue<string[]> LogQueue => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Enable(string programName, string userName, int delayMs)
        {
            try
            {
                if (!Directory.Exists(_rootDirectory))
                    return;

                var directoryPath = _directoryPathBuilder.Build(_rootDirectory, programName);
                _filePath = _filePathBuilder.Build(directoryPath, userName);

                Directory.CreateDirectory(directoryPath);
            }
            finally { }

            var task = Task.Run(() => InitializeWritingToFile(delayMs));
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
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }
    }
}
