using CustomLogs.Models;
using CustomLogs.Sinks;
using System;
using System.Runtime.CompilerServices;

namespace CustomLogs
{
    public interface ICustomLogger : IDisposable
    {
        void Start();
        void Log(string message, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1);
        void LogData(string paramName, object paramValue, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1);
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
            var model = new LogInfo(message, callerPath, callerName, callerLine);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Log(model);
                }
                finally { }
            }
        }

        public void LogData(string paramName, object paramValue, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1)
        {
            var model = new LogDataInfo(paramName, paramValue, callerPath, callerName, callerLine);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogData(model);
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
