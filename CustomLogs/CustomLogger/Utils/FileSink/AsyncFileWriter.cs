using CustomLogs.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CustomLogs.Utils.FileSink
{
    public interface IAsyncFileWriter
    {
        void Start(ConcurrentQueue<LogQueueItem> logQueue, IFilePathBuilder filePathBuilder, int delayMs);
        void WriteLog();
    }

    internal class AsyncFileWriter : IAsyncFileWriter
    {
        private object _padlock = new object();
        private IFilePathBuilder _filePathBuilder;
        private bool _isStarted = false;
        private DateTime _currentDate = default;
        private ConcurrentQueue<LogQueueItem> _logQueue;

        public void Start(ConcurrentQueue<LogQueueItem> logQueue, IFilePathBuilder fileePathBuilder, int delayMs)
        {
            if (_isStarted)
                return;

            _filePathBuilder = fileePathBuilder;
            _logQueue = logQueue;

            Task.Run(async () =>
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
            });

            _isStarted = true;
        }

        public void WriteLog()
        {
            lock (_padlock)
            {

                List<string> totalLines = new List<string>();
                bool isFirstIteration = true;

                while (_logQueue.Count > 0)
                {
                    if (_logQueue.TryDequeue(out LogQueueItem logItem) == false)
                        break;

                    if (isFirstIteration)
                    {
                        totalLines.AddRange(logItem.Lines);
                        _currentDate = logItem.DateTime;
                        isFirstIteration = false;
                        continue;
                    }

                    if (_currentDate.Date == logItem.DateTime.Date)
                    {
                        totalLines.AddRange(logItem.Lines);
                        continue; // it never goes pass this
                    }

                    WriteAndCleanBuffer(totalLines);
                    totalLines.AddRange(logItem.Lines);
                    _currentDate = logItem.DateTime;
                }

                if (totalLines.Count > 0)
                    WriteAndCleanBuffer(totalLines);
            }
        }

        private void WriteAndCleanBuffer(List<string> totalLines)
        {
            var filePath = _filePathBuilder.Build(_currentDate);
            File.AppendAllLines(filePath, totalLines);
            totalLines.Clear();
        }
    }
}
