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

            float rubleToDollar = 89.55f;
            float rubleToYuans = 12.59f;

            float dollarToRubles = 0.011f;
            float dollarToYuans = 0.14f;

            float YuanToRubles = 0.079f;
            float YuanToDollars = 7.11f;

            float rublesCount;
            float dollarsCount;
            float yuansCount;

            int exchangeCurrencyCount;

            string userInput = null;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Введите кол-во рублей: ");
            rublesCount = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите кол-во долларов: ");
            dollarsCount = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите кол-во юаней: ");
            yuansCount = Convert.ToInt32(Console.ReadLine());

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
                        exchangeCurrencyCount = Convert.ToInt32(Console.ReadLine());

                        if (exchangeCurrencyCount <= rublesCount)
                        {
                            dollarsCount += exchangeCurrencyCount / rubleToDollar;
                            rublesCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько рублей.\n");
                        }

                        break;

                    case CommandConvertRublesToYuans:
                        Console.Write("Введите кол-во рублей, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToInt32(Console.ReadLine());

                        if (exchangeCurrencyCount <= rublesCount)
                        {
                            yuansCount += exchangeCurrencyCount / rubleToYuans;
                            rublesCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько рублей.\n");
                        }

                        break;

                    case CommandConvertDollarsToRubles:
                        Console.Write("Введите кол-во долларов, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToInt32(Console.ReadLine());

                        if (exchangeCurrencyCount <= dollarsCount)
                        {
                            rublesCount += exchangeCurrencyCount / dollarToRubles;
                            dollarsCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько долларов.\n");
                        }

                        break;

                    case CommandConvertDollarsToYuans:
                        Console.Write("Введите кол-во долларов, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToInt32(Console.ReadLine());

                        if (exchangeCurrencyCount <= dollarsCount)
                        {
                            yuansCount += exchangeCurrencyCount / dollarToYuans;
                            dollarsCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько долларов.\n");
                        }

                        break;

                    case CommandConvertYuansToRubles:
                        Console.Write("Введите кол-во юаний, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToInt32(Console.ReadLine());

                        if (exchangeCurrencyCount <= yuansCount)
                        {
                            rublesCount += exchangeCurrencyCount / YuanToRubles;
                            yuansCount -= exchangeCurrencyCount;
                        }
                        else
                        {
                            Console.Write("У вас нет столько юаней.\n");
                        }

                        break;

                    case CommandConvertYuansToDollars:
                        Console.Write("Введите кол-во юаний, которое хотите поменять: ");
                        exchangeCurrencyCount = Convert.ToInt32(Console.ReadLine());

                        if (exchangeCurrencyCount <= yuansCount)
                        {
                            dollarsCount += exchangeCurrencyCount / YuanToDollars;
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