using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogs.Utils.FileSink
{
    public interface IFilePathBuilder
    {
        string Build(string directoryPath, string userName);
    }

    public class FilePathBuilder : IFilePathBuilder
    {
        public string Build(string directoryPath, string userName)
        {
            throw new NotImplementedException();
        }
    }
}
