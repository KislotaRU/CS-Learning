using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int maxNumber = 10;

            int[,] arrayNumber = new int[4, 4];

            int rowRequested = 2;
            int columnRequested = 1;

            int sumNumbers = 0;
            int productNumbers = 1;

            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < arrayNumber.GetLength(0); i++)
            {
                for (int j = 0; j < arrayNumber.GetLength(1); j++)
                {
                    arrayNumber[i, j] = random.Next(maxNumber);
                }
            }

            Console.Write($"Двумерный массив:\n\n");

            for (int i = 0; i < arrayNumber.GetLength(0); i++)
            {
                for (int j = 0; j < arrayNumber.GetLength(1); j++)
                {
                    Console.Write($"{arrayNumber[i, j]} ");
                }

                Console.WriteLine();
            }

            for (int i = 0;i < arrayNumber.GetLength(0); i++)
            {
                for (int j = 0;j < arrayNumber.GetLength(1); j++)
                {
                    if (rowRequested - 1 == i)
                        sumNumbers += arrayNumber[i, j];

                    if (columnRequested - 1 == j)
                        productNumbers *= arrayNumber[i, j];
                }
            }

            Console.Write($"\nСумма чисел строки под номером {rowRequested} = {sumNumbers}\n" +
                          $"Произведение чисел столбца под номером {columnRequested} = {productNumbers}\n\n");
        }
    }
}