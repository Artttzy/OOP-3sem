using System;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
