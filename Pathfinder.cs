using System;
using System.Collections.Generic;
using System.IO;

namespace CS_JUNIOR
{
    public class Program
    {
        static void Main()
        {
            Pathfinder pathfinder1 = new Pathfinder(new FileLogWritter());
            Pathfinder pathfinder2 = new Pathfinder(new ConsoleLogWritter());
            Pathfinder pathfinder3 = new Pathfinder(new SecureFileLogWritter());
            Pathfinder pathfinder4 = new Pathfinder(new SecureConsoleLogWritter());
            Pathfinder pathfinder5 = new Pathfinder(new ChainLogWritter(new ConsoleLogWritter(), new SecureFileLogWritter()));

            pathfinder1.Find();
            pathfinder2.Find();
            pathfinder3.Find();
            pathfinder4.Find();
            pathfinder5.Find();
        }
    }

    public interface ILogger
    {
        void WriteError(string message);
    }

    public class ChainLogWritter : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        public ChainLogWritter(params ILogger[] loggers)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
        }

        public void WriteError(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            foreach (var logger in _loggers)
            {
                logger.WriteError(message);
            }
        }
    }

    public class Pathfinder
    {
        private readonly ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Find()
        {
            _logger.WriteError("Запись");
        }
    }

    class FileLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            File.WriteAllText("log.txt", message);
        }
    }

    class ConsoleLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Console.WriteLine(message);
        }
    }

    class SecureFileLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                File.WriteAllText("log.txt", message);
        }
    }

    class SecureConsoleLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                Console.WriteLine(message);
        }
    }
}