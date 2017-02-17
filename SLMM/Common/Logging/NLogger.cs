using System;
using System.Linq;
using System.Threading;
using NLog;

namespace SLMM.Common.Logging
{
    internal class NLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(Exception ex, string message, params object[] args)
        {
            _logger.Error(ex, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void Flush(TimeSpan? maxTimeToWait = null)
        {
            var config = LogManager.Configuration;
            if (config == null)
                return;
            var asyncs = config.AllTargets.OfType<NLog.Targets.Wrappers.AsyncTargetWrapper>().ToArray();
            var countdown = new CountdownEvent(asyncs.Length);
            foreach (var wrapper in asyncs)
            {
                wrapper.Flush(x => countdown.Signal());
            }
            countdown.Wait(maxTimeToWait ?? TimeSpan.FromMilliseconds(500));
        }
    }
}