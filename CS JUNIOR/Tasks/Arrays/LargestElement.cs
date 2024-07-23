using System;

namespace CS_JUNIOR
{
    class LargestElement
    {
        static void Main()
        {
            Random random = new Random();
            int maxRange = 15;
            int minRange = 10;
            int countRows = 10;
            int countColomns = 10;
            int[,] array = new int[countRows, countColomns];
            int maxNumber = int.MinValue;
            int replacement = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(minRange, maxRange);
                    Console.Write($"{array[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (maxNumber < array[i, j])
                    {
                        maxNumber = array[i, j];
                    }
                }
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == maxNumber)
                    {
                        array[i, j] = replacement;
                    }

                    Console.Write($"{array[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"\nНаибольший элемент матрицы = {maxNumber}.");
        }
    }
}