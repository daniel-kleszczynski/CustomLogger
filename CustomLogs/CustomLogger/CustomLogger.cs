using CustomLogs.Models;
using CustomLogs.Sinks;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CustomLogs
{
    public interface ICustomLogger : IDisposable
    {
        void Start();
        void Log(string message, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1);
        void LogData(string paramName, object paramValue, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1);
        void LogDataSet(DataInfo[] dataArray, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLine = -1);
        void LogCollection<TItem>(string collectionName, IEnumerable<TItem> collection, Func<TItem, DataInfo> selector, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1);
        void LogException<T>(T ex, bool isCatched = true) where T : Exception;

    }

    public class CustomLogger : ICustomLogger
    {

        private int _delayMs;
        private string _userName;
        private string _programName;
        private Sink[] _sinks;

        internal CustomLogger(Sink[] sinks, string programName, string userName, int delayMs)
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

        public void LogDataSet(DataInfo[] dataArray, [CallerFilePath]string callerPath = "", [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1)
        {
            var model = new LogDataSetInfo(dataArray, callerPath, callerName, callerLine);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogDataSet(model);
                }
                finally { }
            }
        }

        public void LogCollection<TItem>(string collectionName, IEnumerable<TItem> collection, Func<TItem, DataInfo> selector, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLine = -1)
        {
            var model = new LogCollectionInfo<TItem>(collectionName, collection, selector, callerPath, callerName, callerLine);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogCollection(model);
                }
                finally { }
            }
        }

        public void LogException<TException>(TException ex, bool isCatched = true) where TException : Exception
        {
            var model = new LogExceptionInfo<TException>(ex, isCatched);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogException(model);
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
