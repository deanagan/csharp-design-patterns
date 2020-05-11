using System;

namespace SingletonDi
{
    public sealed class GoFLogger
    {
        // Reference: CSharpInDepth using .Net4 Lazy
        // Implicitly uses LazyThreadSafetyMode.ExecutionAndPublication as the 
        // thread safety mode for the Lazy<GoFLogger>
        private static readonly Lazy<GoFLogger> _lazyLogger = new Lazy<GoFLogger> (
            () => new GoFLogger()
        );
        public static GoFLogger Instance
        {
            get 
            { 
                return _lazyLogger.Value;
            }
        }
    }
}