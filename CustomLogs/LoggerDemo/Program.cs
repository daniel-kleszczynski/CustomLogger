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
            const string ProgramName = nameof(LoggerDemo);
            const string LoggerPath = @"D:\Logs";

            var sinkFactory = new FileSinkFactory();
            var sink = sinkFactory.Create(LoggerPath);

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.Create(new ISink[] { sink }, ProgramName);
            logger.Start();

            Thread.Sleep(500);

            Console.ReadKey();
        }
    }
}
