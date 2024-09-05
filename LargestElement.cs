using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int minNumber = 100;
            int maxNumber = 1000;

            int rowsCount = 10;
            int colomnsCount = 10;

            int[,] arrayNumber = new int[rowsCount, colomnsCount];

            int largestNumberArray = int.MinValue;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Исходная матрица A:\n\n");

            for (int i = 0; i < arrayNumber.GetLength(0); i++)
            {
                for (int j = 0; j < arrayNumber.GetLength(1); j++)
                {
                    arrayNumber[i, j] = random.Next(minNumber, maxNumber);
                    Console.Write($"{arrayNumber[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.Write($"Изменённая матрица A:\n\n");

            for (int i = 0; i < arrayNumber.GetLength(0); i++)
            {
                for (int j = 0; j < arrayNumber.GetLength(1); j++)
                {
                    if (largestNumberArray < arrayNumber[i, j])
                    {
                        largestNumberArray = arrayNumber[i, j];
                        arrayNumber[i, j] = 0;
                    }

                    Console.Write($"{arrayNumber[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.Write($"\nНаибольший элемент матрицы A {largestNumberArray}\n\n");
        }
    }
}