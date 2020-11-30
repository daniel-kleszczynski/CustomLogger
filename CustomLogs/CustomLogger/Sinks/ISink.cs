using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogs.Sinks
{
    public interface ISink : IDisposable
    {
        ConcurrentQueue<string[]> LogQueue { get; }
        void Enable(string programName, string userName, int delayMs);
        void Flush();
    }
}
