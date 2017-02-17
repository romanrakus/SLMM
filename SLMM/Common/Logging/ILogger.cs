using System;

namespace SLMM.Common.Logging
{
    public interface ILogger
    {
        void Trace(string message, params object[] args);
        void Debug(string message, params object[] args);
        void Info(string message, params object[] args);
        void Error(string message, params object[] args);
        void Error(Exception ex, string message, params object[] args);
        void Fatal(string message, params object[] args);

        void Flush(TimeSpan? maxTimeToWait = null);
    }
}
