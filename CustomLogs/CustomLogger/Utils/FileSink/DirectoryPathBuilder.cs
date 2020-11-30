using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogs.Utils.FileSink
{
    public interface IDirectoryPathBuilder
    {
        string Build(string rootDirectory, string programName);
    }

    public class DirectoryPathBuilder : IDirectoryPathBuilder
    {
        public string Build(string rootDirectory, string programName)
        {
            throw new NotImplementedException();
        }
    }
}
