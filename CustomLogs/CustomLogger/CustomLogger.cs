using CustomLogs.Sinks;
using CustomLogs.Utils.FileSink;
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
            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Setup(_programName, _userName, _delayMs);
                }
                finally { }
            }
        }

        public void Log(string message, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLine = -1)
        {
            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Log(message, callerPath, callerName, callerLine);
                }
                finally { }
            }
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
