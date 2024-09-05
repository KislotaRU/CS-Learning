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

            int elementsCount = 30;
            int step = 2;

            int[] arrayNumbers = new int[elementsCount];

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Одномерный массив с кол-во элементов {elementsCount}:\n");

            for ( int i = 0; i < arrayNumbers.Length; i++)
            {
                arrayNumbers[i] = random.Next(minNumber, maxNumber);

                Console.Write($"{arrayNumbers[i]} ");
            }

            Console.Write("\n\nЛокальные максимумы одномерного массива:\n\n");

            if (arrayNumbers[0] > arrayNumbers[1])
                Console.Write($"{arrayNumbers[0]} ");

            for (int i = 1; i < arrayNumbers.Length - 1; i++)
            {
                if (arrayNumbers[i - 1] < arrayNumbers[i] && arrayNumbers[i] > arrayNumbers[i + 1])
                    Console.Write($"{arrayNumbers[i]} ");
            }

            if (arrayNumbers[arrayNumbers.Length - 1] >= arrayNumbers[arrayNumbers.Length - step])
                Console.Write($"{arrayNumbers[arrayNumbers.Length - 1]}\n\n");
        }
    }
}