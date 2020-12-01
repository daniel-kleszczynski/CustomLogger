using CustomLogs;
using CustomLogs.Sinks;
using CustomLogs.Sinks.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = ConfigureLogger();
            logger.Log("test message");

            int x = 7;
            logger.LogData(nameof(x), x);

            Thread.Sleep(500);

            Console.ReadKey();
        }

        private static ICustomLogger ConfigureLogger()
        {
            const string ProgramName = nameof(LoggerDemo);
            const string LoggerPath = @"D:\Logs";

            var sinkFactory = new FileSinkFactory();
            var sink = sinkFactory.Create(LoggerPath);

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.Create(new ISink[] { sink }, ProgramName);

            logger.Start();

            return logger;
        }
    }
}
