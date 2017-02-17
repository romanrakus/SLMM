using System;
using SLMM.Common.Extensions;

namespace SLMM.Common.Logging
{
    public class LogFactory
    {
        private static Func<string, ILogger> _logFactory = name => new NLogger(name);

        public static void SetLogFactory(Func<string, ILogger> factory)
        {
            Ensure.NotNull(factory, nameof(factory));
            _logFactory = factory;
        }

        public static ILogger GetLogger(string name)
        {
            return new LazyLogger(() => _logFactory(name));
        }

        public static ILogger GetLogger<T>()
        {
            return new LazyLogger(() => _logFactory(typeof(T).Name));
        }
    }
}