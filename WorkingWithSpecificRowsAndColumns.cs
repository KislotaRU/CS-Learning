using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int maxNumber = 10;

            int rowsCount = 4;
            int columnsCount = 4;

            int[,] arrayNumber = new int[rowsCount, columnsCount];

            int rowIndex = 2;
            int columnIndex = 1;

            int sum = 0;
            int product = 1;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Двумерный массив:\n\n");

            for (int i = 0; i < arrayNumber.GetLength(0); i++)
            {
                for (int j = 0; j < arrayNumber.GetLength(1); j++)
                {
                    arrayNumber[i, j] = random.Next(maxNumber);
                    Console.Write($"{arrayNumber[i, j]} ");
                }

                Console.WriteLine();
            }

            for (int j = 0; j < arrayNumber.GetLength(1); j++)
            {
                sum += arrayNumber[rowIndex - 1, j];
            }

            for (int i = 0; i < arrayNumber.GetLength(0); i++)
            {
                product *= arrayNumber[i, columnIndex - 1];
            }

            Console.Write($"\nСумма чисел строки под номером {rowIndex} = {sum}\n" +
                          $"Произведение чисел столбца под номером {columnIndex} = {product}\n\n");
        }
    }
}