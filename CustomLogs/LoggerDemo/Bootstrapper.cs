using CustomLogs;
using CustomLogs.Sinks;
using CustomLogs.Sinks.Factories;

namespace LoggerDemo
{
    public class Bootstrapper
    {
        public ICustomLogger ConfigureLogger()
        {
            const string ProgramName = nameof(LoggerDemo);
            const string LoggerPath = @"D:\Logs";

            var sinkFactory = new FileSinkFactory();
            var sink = sinkFactory.Create(LoggerPath);

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.Create(new Sink[] { sink }, ProgramName);

            logger.Start();

            return logger;
        }
    }
}
