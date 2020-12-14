using System;

namespace CustomLogs.Models
{
    public class LogQueueItem
    {
        public DateTime DateTime { get; set; }
        public string[] Lines { get; set; }
    }
}
