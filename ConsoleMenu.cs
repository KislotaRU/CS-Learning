using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            const string CommandPrintInterestingFact = "Интересный факт";
            const string CommandPrintGreeting = "Приветствие";
            const string CommandGenerateRandomNumber = "Число";
            const string CommandClear = "Очистить";
            const string CommandExit = "Выйти";

            Random random = new Random();

            string userInput = null;

            Console.ForegroundColor = ConsoleColor.White;

            while (userInput != CommandExit)
            {
                Console.Write("Доступные команды:\n" +
                             $"\t{CommandPrintInterestingFact} - Показать интересный факт о программировании.\n" +
                             $"\t{CommandPrintGreeting} - Программа поприветсвует введённого пользователя.\n" +
                             $"\t{CommandGenerateRandomNumber} - Сгенерировать случайное число.\n" +
                             $"\t{CommandClear} — Очистить консоль.\n" +
                             $"\t{CommandExit} - Завершить работу программы.\n\n");

                Console.Write("Ожидается ввод команды: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandPrintInterestingFact:
                        Console.Write("Название \"Баг\" произошло от английского слова \"Bug\" - что переводится как \"Жук\".\n" +
                                      "А его значение в программировании стало носить характер какой либо ошибки.\n\n");
                        break;

                    case CommandPrintGreeting:
                        string userName;

                        Console.Write("Введите ваше имя: ");
                        userName = Console.ReadLine();

                        Console.Write($"Консоль приветствует пользователя {userName}\n\n");
                        break;

                    case CommandGenerateRandomNumber:
                        int number;

                        number = random.Next();

                        Console.Write($"Случайное число: {number}\n\n");
                        break;

                    case CommandClear:
                        Console.Clear();
                        break;

                    case CommandExit:
                        Console.Write("Вы завершили программу.\n\n");
                        continue;

                    default:
                        Console.Write("Неизвестная команда.\n\n");
                        break;
                }

                Console.ReadKey();
            }
        }
    }
}