using System;
using System.IO;

namespace CustomLogs.Utils.FileSink
{
    public interface IFilePathBuilder
    {
        void Init(string directoryPath, string programName);
        string Build(DateTime dateTime);
    }

    internal class FilePathBuilder : IFilePathBuilder
    {
        private string _basePath;

        private string BuildBasePath(string directoryPath, string programName)
        {
            return Path.Combine(directoryPath, programName);
        }

        public void Init(string directoryPath, string programName)
        {
            _basePath = BuildBasePath(directoryPath, programName);
        }

        public string Build(DateTime dateTime)
        {
            return $"{_basePath}_{dateTime.ToString("yyyy-MM-dd")}.txt";
        }
    }
}
