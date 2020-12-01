using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CustomLogs.Utils.FileSink
{
    public interface IAsyncFileWriter
    {
        void Start(ConcurrentQueue<string[]> logQueue, string filePath, int delayMs);
        void WriteLog(ConcurrentQueue<string[]> logQueue, string filePath);
    }

    internal class AsyncFileWriter : IAsyncFileWriter
    {
        private bool _isStarted = false;

        public void Start(ConcurrentQueue<string[]> logQueue, string filePath, int delayMs)
        {
            if (_isStarted)
                return;

            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        WriteLog(logQueue, filePath);
                        await Task.Delay(delayMs);
                    }
                    finally { }
                }
            });

            _isStarted = true;
        }

        public void WriteLog(ConcurrentQueue<string[]> logQueue, string filePath)
        {
            List<string> totalLines = new List<string>();


            while (logQueue.Count > 0)
            {
                var result = logQueue.TryDequeue(out string[] lines);

                if (!result)
                    break;

                totalLines.AddRange(lines);
            }

            if (totalLines.Count == 0)
                return;

            File.AppendAllLines(filePath, totalLines);
        }
    }
}
