using CustomLogs;
using CustomLogs.Sinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ProgramName = nameof(LoggerDemo);

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.Create(new ISink[] { new FileSink() }, ProgramName);
        }
    }
}
