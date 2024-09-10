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

            string userInput = null;

            while (userInput != CommandExit)
            {
                Console.Clear();

                Console.Write("Список чисел:\n\n");

                foreach (int number in numbers)
                    Console.Write($"{number} ");

                Console.Write("\n\nДоступные команды:\n" +
                             $"\t{CommandSum}" + "\tВозвращает сумму всех введённых чисел.\n" +
                             $"\t{CommandExit}" + "\tЗавершает работу программы.\n\n");

                Console.Write("Ожидается ввод: ");
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
            }
        }

        static void Sum(List<int> numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
                sum += number;

            Console.Write($"Сумма всех введённых чисел составляет: {sum}\n\n");
        }
    }
}