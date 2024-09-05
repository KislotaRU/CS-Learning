using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            const string CommandSum = "Сложить";
            const string CommandExit = "Выйти";

            string userInput = null;

            int[] numbers = new int[0];

            int number;

            Console.ForegroundColor = ConsoleColor.White;

            while (userInput != CommandExit)
            {
                Console.Write("Массив чисел:\n\n");

                foreach (int temporaryNumber in numbers)
                    Console.Write($"{temporaryNumber} ");

                Console.Write("\n\nПрограмма считывает введённые числа в массив и по команде выводит сумму.\n");

                Console.Write("\nДоступные команды:\n" +
                             $"\t{CommandSum} - Складывает все числа из массива.\n" +
                             $"\t{CommandExit} - Завершает работу программы.\n\n");

                Console.Write("Ожидается ввод: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSum:
                        int sum = 0;

                        foreach (int temporaryNumber in numbers)
                            sum += temporaryNumber;

                        Console.Write($"Сумма всех чисел в массиве = {sum}");
                        break;

                    case CommandExit:
                        Console.Write("Вы завершили работу программы.\n\n");
                        continue;

                    default:
                        if ()
                        break;
                }
            }
        }
    }
}