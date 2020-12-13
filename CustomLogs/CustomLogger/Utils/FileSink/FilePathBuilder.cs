using System;
using System.IO;

namespace CustomLogs.Utils.FileSink
{
    public interface IFilePathBuilder
    {
        string Build(string directoryPath, string programName);
    }

    internal class FilePathBuilder : IFilePathBuilder
    {
        public string Build(string directoryPath, string programName)
        {
            var dateString = DateTime.Now.ToString("yyyy-MM-dd");
            var fileName = programName;

            fileName += $"_{dateString}.txt";

            return Path.Combine(directoryPath, fileName);
        }
    }
}
