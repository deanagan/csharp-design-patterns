using System;

namespace SingletonDI
{
    public sealed class LazyLogger
    {
        // Reference: CSharpInDepth using .Net4 Lazy
        // Implicitly uses LazyThreadSafetyMode.ExecutionAndPublication as the
        // thread safety mode for the Lazy<GoFLogger>
        private static readonly Lazy<Logger> _lazyLogger = new Lazy<Logger>(
            () => new Logger()
        );
        public static Logger Instance
        {
            get
            {
                return _lazyLogger.Value;
            }
        }
    }
}
