using CustomLogs;
using CustomLogs.Sinks;
using CustomLogs.Sinks.Factories;
using System.Configuration;

namespace LoggerDemo
{
    public class Bootstrapper
    {
        public ICustomLogger ConfigureLogger()
        {
            const string ProgramName = nameof(LoggerDemo);

            var sinkFactory = new FileSinkFactory();
            var loggerPath = ConfigurationManager.AppSettings["loggerRootPath"];
            var sink = sinkFactory.Create(loggerPath);

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.Create(new Sink[] { sink }, ProgramName);

            logger.Start();

            return logger;
        }
    }
}
