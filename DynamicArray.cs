using System;

/*
 * Пользователь вводит числа, и программа их запоминает.
 * Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.
 * Выход из программы должен происходить только в том случае, если пользователь введет команду exit.
 * Если введено не sum и не exit, значит это число и его надо добавить в массив.
 * В начале цикла надо выводить в консоль все числа, которые содержатся в массиве, а значит их ввел пользователь ранее.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            string userInput = null;

            int[] numbers = new int[0];

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
                        if (int.TryParse(userInput, out int number))
                        {
                            int[] temporaryArrayNumbers = new int[numbers.Length + 1];

                            for (int i = 0; i < numbers.Length; i++)
                                temporaryArrayNumbers[i] = numbers[i];

                            temporaryArrayNumbers[temporaryArrayNumbers.Length - 1] = number;

                            numbers = temporaryArrayNumbers;
                        }
                        else
                        {
                            Console.Write("Требуется ввести число или команду.\n");
                        }

                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}