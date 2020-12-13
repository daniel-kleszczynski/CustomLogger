using System;

namespace CustomLogs.Models
{
    public class LogExceptionInfo<T> where T : Exception
    {
        public LogExceptionInfo(T exception, bool isCatched, string userName)
        {
            Exception = exception;
            IsCatched = isCatched;
        }

        public T Exception { get; private set; }
        public bool IsCatched { get; private set; }
    }
}
