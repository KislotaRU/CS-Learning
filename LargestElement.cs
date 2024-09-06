using System;

/*
 * Найти наибольший элемент матрицы A(10,10) и записать ноль в те ячейки,
 * где он находятся. Вывести наибольший элемент, исходную и полученную матрицу.
*/

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

            int replacement = 0;

            int[,] numbers = new int[rowsCount, colomnsCount];

            int largestNumberArray = int.MinValue;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Исходная матрица A:\n\n");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(minNumber, maxNumber);
                    Console.Write($"{numbers[i, j]} ");

                    if (largestNumberArray < numbers[i, j])
                        largestNumberArray = numbers[i, j];
                }

                Console.WriteLine();
            }

            Console.Write($"\nИзменённая матрица A:\n\n");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (largestNumberArray == numbers[i, j])
                        numbers[i, j] = replacement;

                    Console.Write($"{numbers[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.Write($"\nНаибольший элемент матрицы A {largestNumberArray}\n\n");
        }
    }
}