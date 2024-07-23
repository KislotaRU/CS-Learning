using System;

namespace CS_JUNIOR
{
    class CurrencyConverter
    {
        static void Main(string[] args)
        {
            string inputUser = "";
            const string CommandYuanToRuble = "1";
            const string CommandYuanToDollar = "2";
            const string CommandRubleToYuan = "3";
            const string CommandRubleToDollar = "4";
            const string CommandDollarToYuan = "5";
            const string CommandDollarToRuble = "6";
            const string CommandExit = "exit";
            float tempMoney;
            float yuans;
            float yuanToRuble = 8.76f;
            float yuanToDollar = 0.14f;
            float rubles;
            float rubleToYuan = 0.11f;
            float rubleToDollar = 0.016f;
            float dollars;
            float dollarToYuan = 6.93f;
            float dollarToRuble = 61.5f;

            Console.WriteLine("Вас приветствует обменник валют.");

            Console.Write("Введите кол-во Юаней на вашем балансе: ");
            yuans = Convert.ToSingle(Console.ReadLine());
            Console.Write("Введите кол-во Рублей на вашем балансе: ");
            rubles = Convert.ToSingle(Console.ReadLine());
            Console.Write("Введите кол-во Долларов на вашем балансе: ");
            dollars = Convert.ToSingle(Console.ReadLine());

            while (inputUser != CommandExit)
            {
                Console.WriteLine($"\nВаше кол-во Юаней: {yuans}");
                Console.WriteLine($"Ваше кол-во Рублей: {rubles}");
                Console.WriteLine($"Ваше кол-во Долларов: {dollars}");

                Console.WriteLine("\nВведите одну из команд: " +
                  $"\n\t{CommandYuanToRuble} - Обменять Юань на Рубли." +
                  $"\n\t{CommandYuanToDollar} - Обменять Юань на Доллары." +
                  $"\n\t{CommandRubleToYuan} - Обменять Рубли на Юани." +
                  $"\n\t{CommandRubleToDollar} - Обменять Рубли на Доллары." +
                  $"\n\t{CommandDollarToYuan} - Обменять Доллары на Юани." +
                  $"\n\t{CommandDollarToRuble} - Обменять Доллары на Рубли." +
                  $"\n\t{CommandExit} - Завершить программу.");

                Console.Write("Команда: ");
                inputUser = Console.ReadLine();

                switch (inputUser)
                {
                    case CommandYuanToRuble:
                        Console.WriteLine("Обменять Юань на Рубли.");
                        Console.WriteLine($"Ваше кол-во Юаней: {yuans}");
                        Console.Write("Какую сумму хотите обменять: ");
                        tempMoney = Convert.ToSingle(Console.ReadLine());

                        if (yuans >= tempMoney)
                        {
                            rubles += tempMoney * yuanToRuble;
                            yuans -= tempMoney;
                        }
                        else
                        {
                            Console.WriteLine("Не хватат денег для обмена.");
                        }

                        break;
                    case CommandYuanToDollar:
                        Console.WriteLine("Обменять Юань на Доллары.");
                        Console.WriteLine($"Ваше кол-во Юаней: {yuans}");
                        Console.Write("Какую сумму хотите обменять: ");
                        tempMoney = Convert.ToSingle(Console.ReadLine());

                        if (yuans >= tempMoney)
                        {
                            dollars += tempMoney * yuanToDollar;
                            yuans -= tempMoney;
                        }
                        else
                        {
                            Console.WriteLine("Не хватат денег для обмена.");
                        }

                        break;
                    case CommandRubleToYuan:
                        Console.WriteLine("Обменять Рубли на Юани.");
                        Console.WriteLine($"Ваше кол-во Рублей: {rubles}");
                        Console.Write("Какую сумму хотите обменять: ");
                        tempMoney = Convert.ToSingle(Console.ReadLine());

                        if (rubles >= tempMoney)
                        {
                            yuans += tempMoney * rubleToYuan;
                            rubles -= tempMoney;
                        }
                        else
                        {
                            Console.WriteLine("Не хватат денег для обмена.");
                        }

                        break;
                    case CommandRubleToDollar:
                        Console.WriteLine("Обменять Рубли на Доллары.");
                        Console.WriteLine($"Ваше кол-во Рублей: {rubles}");
                        Console.Write("Какую сумму хотите обменять: ");
                        tempMoney = Convert.ToSingle(Console.ReadLine());

                        if (rubles >= tempMoney)
                        {
                            dollars += tempMoney * rubleToDollar;
                            rubles -= tempMoney;
                        }
                        else
                        {
                            Console.WriteLine("Не хватат денег для обмена.");
                        }

                        break;
                    case CommandDollarToYuan:
                        Console.WriteLine("Обменять Доллары на Юань.");
                        Console.WriteLine($"Ваше кол-во Долларов: {dollars}");
                        Console.Write("Какую сумму хотите обменять: ");
                        tempMoney = Convert.ToSingle(Console.ReadLine());

                        if (dollars >= tempMoney)
                        {
                            yuans += tempMoney * dollarToYuan;
                            dollars -= tempMoney;

                        }
                        else
                        {
                            Console.WriteLine("Не хватат денег для обмена.");
                        }

                        break;
                    case CommandDollarToRuble:
                        Console.WriteLine("Обменять Доллары на Рубли.");
                        Console.WriteLine($"Ваше кол-во Долларов: {dollars}");
                        Console.Write("Какую сумму хотите обменять: ");
                        tempMoney = Convert.ToSingle(Console.ReadLine());

                        if (dollars >= tempMoney)
                        {
                            rubles += tempMoney * dollarToRuble;
                            dollars -= tempMoney;
                        }
                        else
                        {
                            Console.WriteLine("Не хватат денег для обмена.");
                        }

                        break;
                    case CommandExit:
                        Console.WriteLine("Завершение программы...");
                        inputUser = CommandExit;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}