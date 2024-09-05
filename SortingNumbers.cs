using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int minNumber = 10;
            int maxNumber = 100;

            int numbersCount = 20;

            int[] numbers = new int[numbersCount];

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Исходный массив:\n\n");

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minNumber, maxNumber);
                Console.Write($"{numbers[i]} ");
            }

            Console.Write("\n\nМассив после сортировки:\n\n");

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    int temporaryNumber;

                    if (numbers[j] > numbers[j + 1])
                    {
                        temporaryNumber = numbers[j + 1];
                        numbers[j + 1] = numbers[j];
                        numbers[j] = temporaryNumber;
                    }
                }
            }

            foreach (int number in numbers)
                Console.Write($"{number} ");

            Console.Write("\n\n");
        }
    }
}