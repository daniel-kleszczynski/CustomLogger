using CustomLogs.Utils.FileSink;

namespace CustomLogs.Sinks.Factories
{
    public class FileSinkFactory
    {
        public FileSink Create(string rootDirectory)
        {
            var directoryPathBuilder = new DirectoryPathBuilder();
            var filePathBuilder = new FilePathBuilder();

            return new FileSink(rootDirectory, directoryPathBuilder, filePathBuilder);
        }
    }
}
