using CustomLogs.Utils.FileSink;

namespace CustomLogs.Sinks.Factories
{
    public class FileSinkFactory
    {
        public FileSink Create(string rootDirectory)
        {
            var filePathBuilder = new FilePathBuilder();
            var logFormatter = new LogFormatter();
            var fileWriter = new AsyncFileWriter();

            return new FileSink(rootDirectory, filePathBuilder, logFormatter, fileWriter);
        }
    }
}
