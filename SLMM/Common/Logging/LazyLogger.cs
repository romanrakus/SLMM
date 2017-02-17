using System;
using SLMM.Common.Extensions;

namespace SLMM.Common.Logging
{
    internal class LazyLogger : ILogger
    {
        private readonly Lazy<ILogger> _logger;

        public LazyLogger(Func<ILogger> factory)
        {
            Ensure.NotNull(factory, nameof(factory));
            _logger = new Lazy<ILogger>(factory);
        }

        public void Trace(string message, params object[] args)
        {
            _logger.Value.Trace(message, args);
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Value.Debug(message, args);
        }

        public void Info(string message, params object[] args)
        {
            _logger.Value.Info(message, args);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Value.Error(message, args);
        }

        public void Error(Exception ex, string message, params object[] args)
        {
            _logger.Value.Error(ex, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Value.Fatal(message, args);
        }

        public void Flush(TimeSpan? maxTimeToWait = null)
        {
            _logger.Value.Flush(maxTimeToWait);
        }
    }
}