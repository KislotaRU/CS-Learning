using System;
using System.Collections.Generic;
using System.IO;

namespace CS_JUNIOR
{
    public class Program
    {
        static void Main()
        {
            Pathfinder pathfinder1 = new Pathfinder(new FileLogWritter(new Logger()));
            Pathfinder pathfinder2 = new Pathfinder(new ConsoleLogWritter(new Logger()));
            Pathfinder pathfinder3 = new Pathfinder(new SecureLogWritter(new FileLogWritter(new Logger())));
            Pathfinder pathfinder4 = new Pathfinder(new SecureLogWritter(new ConsoleLogWritter(new Logger())));
            Pathfinder pathfinder5 = new Pathfinder(new ChainLogWritter(new ConsoleLogWritter(new Logger()), new SecureLogWritter(new FileLogWritter(new Logger()))));

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

    public class Logger : ILogger
    {
        public void WriteError(string message) { }
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
        private readonly ILogger _logger;
        private readonly string _path = "log.txt";

        public FileLogWritter(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void WriteError(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            _logger.WriteError(message);

            File.WriteAllText(_path, message);
        }
    }

    class ConsoleLogWritter : ILogger
    {
        private readonly ILogger _logger;

        public ConsoleLogWritter(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void WriteError(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            _logger.WriteError(message);

            Console.WriteLine(message);
        }
    }

    public abstract class LoggingPolicy : ILogger
    {
        private readonly ILogger _logger;

        protected LoggingPolicy(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual void WriteError(string message)
        {
            if (AllowLog())
                _logger.WriteError(message);
        }

        protected abstract bool AllowLog();
    }

    class SecureLogWritter : LoggingPolicy
    {
        public SecureLogWritter(ILogger logger) : base(logger) { }

        private bool IsFriday => DateTime.Now.DayOfWeek == DayOfWeek.Friday;

        public override void WriteError(string message)
        {
            base.WriteError(message);
        }

        protected override bool AllowLog()
        {
            return IsFriday;
        }
    }
}