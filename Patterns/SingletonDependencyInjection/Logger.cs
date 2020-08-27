using System;

namespace SingletonDI
{
    public class Logger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine($"LOG: {msg}");
        }
    }
}
