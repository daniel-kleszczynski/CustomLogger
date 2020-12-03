using CustomLogs;
using CustomLogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerDemo
{
    public class LogPresenter
    {
        public void SimpleLogDemo(ICustomLogger logger)
        {
            logger.Log("Simple log example");
        }

        public void LogDataDemo(ICustomLogger logger)
        {
            int x = 7;
            logger.LogData(nameof(x), x);
        }

        public void LogDataSet(ICustomLogger logger)
        {
            int number = 13;
            string name = "Tony";
            
            logger.LogDataSet(new DataInfo[]
            {
                new DataInfo(nameof(number), number),
                new DataInfo(nameof(name), name)
            });
        }

        public void LogCollection(ICustomLogger logger)
        {
            string[] colors = new string[] { "red", "green", "blue" };
            logger.LogCollection(nameof(colors), colors, c => new DataInfo(string.Empty, c));

            ExampleModel[] models = new ExampleModel[]
            {
                new ExampleModel {Age = 18, Name = "Adam"},
                new ExampleModel {Age = 24, Name = "Sara"}
            };

            logger.LogCollection(nameof(models), models, i => new DataInfo($"{nameof(i.Name)}", $"{i.Name}"));
        }
    }
}
