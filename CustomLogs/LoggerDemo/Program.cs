using System;

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
                presenter.LogErrorDemo(logger);
            }

            Console.ReadKey();
        }

        
    }
}
