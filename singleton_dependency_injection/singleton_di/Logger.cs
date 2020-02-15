using System;

namespace SingletonDi
{
    public class Logger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine($"LOG: {msg}");
        }
    }
}