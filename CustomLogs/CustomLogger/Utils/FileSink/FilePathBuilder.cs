using System;
using System.IO;

namespace CustomLogs.Utils.FileSink
{
    public interface IFilePathBuilder
    {
        string Build(string directoryPath, string programName, string userName);
    }

    internal class FilePathBuilder : IFilePathBuilder
    {
        public string Build(string directoryPath, string programName, string userName)
        {
            var dateString = DateTime.Now.ToString("yyyy-MM-dd");
            var fileName = programName;

            if (!string.IsNullOrEmpty(userName))
                fileName += '_' + userName;

            fileName += $"_{dateString}.txt";

            return Path.Combine(directoryPath, fileName);
        }
    }
}
