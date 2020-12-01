using CustomLogs.Utils.FileSink;

namespace CustomLogs.Sinks.Factories
{
    public class FileSinkFactory
    {
        public FileSink Create(string rootDirectory)
        {
            var filePathBuilder = new FilePathBuilder();

            return new FileSink(rootDirectory, filePathBuilder);
        }
    }
}
