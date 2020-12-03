using CustomLogs;
using CustomLogs.Models;
using CustomLogs.Sinks;
using CustomLogs.Sinks.Factories;
using CustomLogs.Utils.FileSink;
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
            var bootstrapper = new Bootstrapper();
            var presenter = new LogPresenter();
            
            using (var logger = bootstrapper.ConfigureLogger())
            {
                presenter.SimpleLogDemo(logger);
                presenter.LogDataDemo(logger);
                presenter.LogDataSetDemo(logger);
                presenter.LogCollectionDemo(logger);
                presenter.LogExceptionDemo(logger);
            }

            Console.ReadKey();
        }

        
    }
}
