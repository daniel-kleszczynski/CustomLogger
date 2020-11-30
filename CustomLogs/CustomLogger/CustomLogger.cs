using CustomLogs.Sinks;
using System;
using System.Runtime.CompilerServices;

namespace CustomLogs
{
    public interface ICustomLogger : IDisposable
    {
        void Start();
        void Log(string message, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1);
    }

    public class CustomLogger : ICustomLogger
    {

        private int _delayMs;
        private string _userName;
        private string _programName;
        private ISink[] _sinks;

        internal CustomLogger(ISink[] sinks, string programName, string userName, int delayMs)
        {
            _sinks = sinks;
            _programName = programName;
            _userName = userName;
            _delayMs = delayMs;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Log(string message, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLine = -1)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Dispose();
                }
                finally { }
            }
        }
    }
}
