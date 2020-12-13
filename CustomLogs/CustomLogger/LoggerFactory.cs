using CustomLogs.Sinks;

namespace CustomLogs
{
    public class LoggerFactory
    {
        private const int DefaultDelayMs = 250;
        private const string NoUser = null;

        private static readonly object _padlock = new object();
        private static CustomLogger _instance;

        public ICustomLogger Create(Sink[] sinks, string programName)
        {
            return Create(sinks, programName, NoUser, DefaultDelayMs);
        }

        public ICustomLogger Create(Sink[] sinks, string programName, string userName)
        {
            return Create(sinks, programName, userName, DefaultDelayMs);
        }

        public ICustomLogger Create(Sink[] sinks, string programName, int delayMs)
        {
            return Create(sinks, programName, NoUser, delayMs);
        }

        public ICustomLogger Create(Sink[] sinks, string programName, string userName, int delayMs)
        {
            lock (_padlock)
            {
                if (_instance == null)
                    _instance = new CustomLogger(sinks, programName, delayMs);
            }

            return _instance;
        }
    }
}
