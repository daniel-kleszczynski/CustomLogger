using System;

namespace CustomLogs.Models
{
    public class LogExceptionInfo<T> where T : Exception
    {
        public LogExceptionInfo(T exception, bool isCatched, string userName)
        {
            Exception = exception;
            IsCatched = isCatched;
            UserName = userName;
            DateTime = DateTime.Now;
        }

        public T Exception { get; private set; }
        public bool IsCatched { get; private set; }
        public string UserName { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
