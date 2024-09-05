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

            int[] arrayNumbers = new int[numbersCount];

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Исходный массив:\n\n");

            for (int i = 0; i < arrayNumbers.Length; i++)
            {
                arrayNumbers[i] = random.Next(minNumber, maxNumber);
                Console.Write($"{arrayNumbers[i]} ");
            }

            Console.Write("\n\nМассив после сортировки:\n\n");

            for (int i = 0; i < arrayNumbers.Length; i++)
            {
                for (int j = 0; j < arrayNumbers.Length - 1; j++)
                {
                    int temporaryNumber;

                    if (arrayNumbers[j] > arrayNumbers[j + 1])
                    {
                        temporaryNumber = arrayNumbers[j + 1];
                        arrayNumbers[j + 1] = arrayNumbers[j];
                        arrayNumbers[j] = temporaryNumber;
                    }
                }
            }

            foreach (int number in arrayNumbers)
                Console.Write($"{number} ");

            Console.Write("\n\n");
        }
    }
}