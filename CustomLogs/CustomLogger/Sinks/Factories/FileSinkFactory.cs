using CustomLogs.Utils.FileSink;

namespace CustomLogs.Sinks.Factories
{
    public class FileSinkFactory
    {
        public FileSink Create(string rootDirectory)
        {
            var filePathBuilder = new FilePathBuilder();
            var fileWriter = new AsyncFileWriter();

            return new FileSink(rootDirectory, filePathBuilder, fileWriter);
        }
    }
}
