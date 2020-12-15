using CustomLogs.Models;
using CustomLogs.Sinks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CustomLogs
{
    public interface ICustomLogger : IDisposable
    {
        void Start();
        void Log(string message, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void Log(string message, string userName, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void LogData(string paramName, object paramValue, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void LogData(string paramName, object paramValue, string userName, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void LogDataSet(DataInfo[] dataArray, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void LogDataSet(DataInfo[] dataArray, string userName, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void LogCollection<TItem>(string collectionName, IEnumerable<TItem> collection, Func<TItem, DataInfo> selector, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void LogCollection<TItem>(string collectionName, IEnumerable<TItem> collection, Func<TItem, DataInfo> selector, string userName, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void LogException<T>(T ex, bool isCatched = true) where T : Exception;
        void LogException<T>(T ex, string userName, bool isCatched = true) where T : Exception;
        void LogError(string message, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
        void LogError(string message, string userName, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "");
    }

    public class CustomLogger : ICustomLogger
    {
        private const string NoUser = null;

        private int _delayMs;
        private string _programName;
        private Sink[] _sinks;

        internal CustomLogger(Sink[] sinks, string programName, int delayMs)
        {
            _sinks = sinks;
            _programName = programName;
            _delayMs = delayMs;
        }

        public void Start()
        {
            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Setup(_programName, _delayMs);
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(_programName, ex.Message, EventLogEntryType.Error);
                }
            }
        }

        public void Log(string message,
                        [CallerLineNumber] int callerLine = -1,
                        [CallerFilePath] string callerPath = "",
                        [CallerMemberName] string callerName = "")
        {
            Log(message, NoUser, callerLine, callerPath, callerName);
        }

        public void Log(string message,
                        string userName,
                        [CallerLineNumber] int callerLine = -1,
                        [CallerFilePath] string callerPath = "",
                        [CallerMemberName] string callerName = "")
        {
            var model = new LogInfo(message, callerPath, callerName, callerLine, userName);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Log(model);
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(_programName, ex.Message, EventLogEntryType.Error);
                }
            }
        }

        public void LogData(string paramName,
                            object paramValue,
                           [CallerLineNumber] int callerLine = -1,
                           [CallerFilePath] string callerPath = "",
                           [CallerMemberName] string callerName = "")
        {
            LogData(paramName, paramValue, NoUser, callerLine, callerPath, callerName);
        }

        public void LogData(string paramName,
                            object paramValue,
                            string userName,
                            [CallerLineNumber] int callerLine = -1,
                            [CallerFilePath] string callerPath = "",
                            [CallerMemberName] string callerName = "")
        {
            var model = new LogDataInfo(paramName, paramValue, callerPath, callerName, callerLine, userName);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogData(model);
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(_programName, ex.Message, EventLogEntryType.Error);
                }
            }
        }

        public void LogDataSet(DataInfo[] dataArray,
                               [CallerLineNumber] int callerLine = -1,
                               [CallerFilePath] string callerPath = "",
                               [CallerMemberName] string callerName = "")
        {
            LogDataSet(dataArray, NoUser, callerLine, callerPath, callerName);
        }

        public void LogDataSet(DataInfo[] dataArray,
                               string userName,
                               [CallerLineNumber] int callerLine = -1,
                               [CallerFilePath] string callerPath = "",
                               [CallerMemberName] string callerName = "")
        {
            var model = new LogDataSetInfo(dataArray, callerPath, callerName, callerLine, userName);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogDataSet(model);
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(_programName, ex.Message, EventLogEntryType.Error);
                }
            }
        }

        public void LogCollection<TItem>(string collectionName,
                                        IEnumerable<TItem> collection,
                                        Func<TItem, DataInfo> selector,
                                        [CallerLineNumber] int callerLine = -1,
                                        [CallerFilePath] string callerPath = "",
                                        [CallerMemberName] string callerName = "")
        {
            LogCollection(collectionName, collection, selector, NoUser, callerLine, callerPath, callerName);
        }

        public void LogCollection<TItem>(string collectionName,
                                         IEnumerable<TItem> collection,
                                         Func<TItem, DataInfo> selector,
                                         string userName,
                                         [CallerLineNumber] int callerLine = -1,
                                         [CallerFilePath] string callerPath = "",
                                         [CallerMemberName] string callerName = "")
        {
            var model = new LogCollectionInfo<TItem>(collectionName, collection, selector, userName, callerPath,
                                                     callerName, callerLine);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogCollection(model);
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(_programName, ex.Message, EventLogEntryType.Error);
                }
            }
        }

        public void LogException<TException>(TException ex, bool isCatched = true) where TException : Exception
        {
            LogException(ex, NoUser, isCatched);
        }

        public void LogException<TException>(TException ex, string userName, bool isCatched = true)
            where TException : Exception
        {
            var model = new LogExceptionInfo<TException>(ex, isCatched, userName);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogException(model);
                }
                catch (Exception exp)
                {
                    EventLog.WriteEntry(_programName, exp.Message, EventLogEntryType.Error);
                }
            }
        }

        public void LogError(string message, [CallerLineNumber] int callerLine = -1, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerName = "")
        {
            LogError(message, NoUser, callerLine, callerPath, callerName);
        }

        public void LogError(string message,
                             string userName,
                             [CallerLineNumber] int callerLine = -1,
                             [CallerFilePath] string callerPath = "",
                             [CallerMemberName] string callerName = "")
        {
            var model = new LogInfo(message, callerPath, callerName, callerLine, userName);

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.LogError(model);
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(_programName, ex.Message, EventLogEntryType.Error);
                }
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
                catch (Exception ex)
                {
                    EventLog.WriteEntry(_programName, ex.Message, EventLogEntryType.Error);
                }
            }
        }
    }
}
