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
            Pathfinder pathfinder3 = new Pathfinder(new SecureLogWritter(new FileLogWritter()));
            Pathfinder pathfinder4 = new Pathfinder(new SecureLogWritter(new ConsoleLogWritter()));
            Pathfinder pathfinder5 = new Pathfinder(new ChainLogWritter(new ConsoleLogWritter(), new SecureLogWritter(new FileLogWritter())));

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

            foreach (ILogger logger in _loggers)
            {
                if (logger == null)
                    throw new ArgumentNullException(nameof(loggers));
            }
        }

        public void WriteError(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException(nameof(message));

            foreach (var logger in _loggers)
                logger.WriteError(message);
        }
    }

    public class Pathfinder
    {
        private readonly ILogger _logger;
        private readonly string _message = "Запись";

        public Pathfinder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Find()
        {
            _logger.WriteError(_message);
        }
    }

    class FileLogWritter : ILogger
    {
        private readonly string _path = "log.txt";

        public void WriteError(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException(nameof(message));

            File.WriteAllText(_path, message);
        }
    }

    class ConsoleLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException(nameof(message));

            Console.WriteLine(message);
        }
    }

    class SecureLogWritter : ILogger
    {
        private readonly ILogger _logger;
        private readonly DayOfWeek _targetDay = DayOfWeek.Friday;

        public SecureLogWritter(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private bool IsAllow => DateTime.Now.DayOfWeek == _targetDay;

        public void WriteError(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException(nameof(message));

            if (IsAllow)
                _logger.WriteError(message);
        }

        protected bool AllowLog()
        {
            return IsAllow;
        }
    }
}