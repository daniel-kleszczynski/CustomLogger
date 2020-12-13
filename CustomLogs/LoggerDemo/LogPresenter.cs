using CustomLogs;
using CustomLogs.Models;
using System;

namespace LoggerDemo
{
    public class LogPresenter
    {
        private const string User = "Lucky";

        public void SimpleLogDemo(ICustomLogger logger)
        {
            logger.Log("Simple log example", User);
        }

        public void LogDataDemo(ICustomLogger logger)
        {
            int x = 7;
            logger.LogData(nameof(x), x, User);
        }

        public void LogDataSetDemo(ICustomLogger logger)
        {
            int number = 13;
            string name = "Tony";
            
            logger.LogDataSet(new DataInfo[]
            {
                new DataInfo(nameof(number), number),
                new DataInfo(nameof(name), name)
            }, User);
        }

        public void LogCollectionDemo(ICustomLogger logger)
        {
            string[] colors = new string[] { "red", "green", "blue" };
            logger.LogCollection(nameof(colors), colors, c => new DataInfo(string.Empty, c));

            ExampleModel[] models = new ExampleModel[]
            {
                new ExampleModel {Age = 18, Name = "Adam"},
                new ExampleModel {Age = 24, Name = "Sara"}
            };

            logger.LogCollection(nameof(models), models, i => new DataInfo($"{nameof(i.Name)}", $"{i.Name}"), User);
        }

        public void LogExceptionDemo(ICustomLogger logger)
        {
            try
            {
                Exception innerException = new Exception("Example Inner exception");
                throw new InvalidOperationException("Operation is not permitted.", innerException);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogException(ex, User);
            }
        }

        public void LogErrorDemo(ICustomLogger logger)
        {
            logger.LogError(@"You typed incorrect number", User);
        }
    }
}
