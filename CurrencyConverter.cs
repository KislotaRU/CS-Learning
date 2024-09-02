using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            const string CommandConvertRublesToDollars = "Рубли в доллары";
            const string CommandConvertRublesToYuans = "Рубли в юани";
            const string CommandConvertDollarsToRubles = "Доллары в рубли";
            const string CommandConvertDollarsToYuans = "Доллары в юани";
            const string CommandConvertYuansToRubles = "Юани в рубли";
            const string CommandConvertYuansToDollars = "Юани в доллары";
            const string CommandExit = "Выйти";

            float rublesToDollars = 89.55f;
            float rublsToYuans = 12.59f;

            float dollarsToRubles = 0.011f;
            float dollarsToYuans = 0.14f;

            float YuansToRubles = 0.079f;
            float YuansToDollars = 7.11f;

            float rublesCount;
            float dollarsCount;
            float yuansCount;

            float exchangeCurrencyCount;

            string userInput = null;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Введите кол-во рублей: ");
            rublesCount = Convert.ToSingle(Console.ReadLine());

            Console.Write("Введите кол-во долларов: ");
            dollarsCount = Convert.ToSingle(Console.ReadLine());

            Console.Write("Введите кол-во юаней: ");
            yuansCount = Convert.ToSingle(Console.ReadLine());

            while (userInput != CommandExit)
            {
                Console.Clear();

                Console.Write("Ваш баланс: \n" + 
                             $"\t{rublesCount} Р".PadRight(15) + 
                             $"{dollarsCount} $".PadRight(15) + 
                             $"{yuansCount} Y".PadRight(15));

                Console.Write("\n\nДоступные команды:\n" +
                             $"\t{CommandConvertRublesToDollars}".PadRight(16) + " - конвертирует рубли в доллары.\n" +
                             $"\t{CommandConvertRublesToYuans}".PadRight(16) + " - конвертирует рубли в юани.\n" +
                             $"\t{CommandConvertDollarsToRubles}".PadRight(16) + " - конвертирует доллары в рубли.\n" +
                             $"\t{CommandConvertDollarsToYuans}".PadRight(16) + " - конвертирует доллары в доллары.\n" +
                             $"\t{CommandConvertYuansToRubles}".PadRight(16) + " - конвертирует юани в рубли.\n" +
                             $"\t{CommandConvertYuansToDollars}".PadRight(16) + " - конвертирует юани в доллары.\n" +
                             $"\t{CommandExit}".PadRight(16) + " - Завершить работу программы.\n\n");

                Console.Write("Ожидается ввод команды: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandConvertRublesToDollars:
                        Console.Write("Введите кол-во рублей, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (exchangeCurrencyCount <= rublesCount)
                        {
                            dollarsCount += exchangeCurrencyCount / rublesToDollars;
                            rublesCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько рублей.\n");
                        }

                        break;

                    case CommandConvertRublesToYuans:
                        Console.Write("Введите кол-во рублей, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (exchangeCurrencyCount <= rublesCount)
                        {
                            yuansCount += exchangeCurrencyCount / rublsToYuans;
                            rublesCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько рублей.\n");
                        }

                        break;

                    case CommandConvertDollarsToRubles:
                        Console.Write("Введите кол-во долларов, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (exchangeCurrencyCount <= dollarsCount)
                        {
                            rublesCount += exchangeCurrencyCount / dollarsToRubles;
                            dollarsCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько долларов.\n");
                        }

                        break;

                    case CommandConvertDollarsToYuans:
                        Console.Write("Введите кол-во долларов, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (exchangeCurrencyCount <= dollarsCount)
                        {
                            yuansCount += exchangeCurrencyCount / dollarsToYuans;
                            dollarsCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько долларов.\n");
                        }

                        break;

                    case CommandConvertYuansToRubles:
                        Console.Write("Введите кол-во юаний, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (exchangeCurrencyCount <= yuansCount)
                        {
                            rublesCount += exchangeCurrencyCount / YuansToRubles;
                            yuansCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько юаней.\n");
                        }

                        break;

                    case CommandConvertYuansToDollars:
                        Console.Write("Введите кол-во юаний, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (exchangeCurrencyCount <= yuansCount)
                        {
                            dollarsCount += exchangeCurrencyCount / YuansToDollars;
                            yuansCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько юаней.\n");
                        }

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