using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            List<int> numbers = new List<int>();

            string[] menu = new string[]
            {
                CommandSum,
                CommandExit
            };

            string userInput = null;

            while (userInput != CommandExit)
            {
                Console.Write("Программа считывает числа и по команде возвращает сумму этих чисел.\n");

                ShowList(numbers);

                Console.Write("\n\nДоступные команды:\n");
                PrintMenu(menu);

                Console.Write("\nОжидается ввод: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSum:
                        Sum(numbers);
                        break;

                    case CommandExit:
                        Console.Write("Вы завершили работу программы.\n\n");
                        continue;

                    default:
                        if (int.TryParse(userInput, out int number))
                            numbers.Add(number);
                        else
                            Console.Write("Требуется ввести команду или число.\n\n");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static void Sum(List<int> numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
                sum += number;

            Console.Write($"Сумма всех введённых чисел составляет: {sum}\n\n");
        }

        static void ShowList(List<int> numbers)
        {
            Console.Write("Список чисел:\n");

            foreach (int number in numbers)
                Console.Write($"{number} ");
        }

        static void PrintMenu(string[] menu)
        {
             for (int i = 0; i < menu.Length; i++)
                Console.Write($"\t{i + 1}. {menu[i]}\n");
        }
    }
}