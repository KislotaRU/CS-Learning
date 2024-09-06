using System;

/*
 * Дан двумерный массив.
 * Вычислить сумму второй строки и произведение первого столбца.
 * Вывести исходную матрицу и результаты вычислений. 
*/

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

            int[,] numbers = new int[rowsCount, columnsCount];

            int rowIndex = 2;
            int columnIndex = 1;

            int sum = 0;
            int product = 1;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Двумерный массив:\n\n");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(maxNumber);
                    Console.Write($"{numbers[i, j]} ");
                }

                Console.WriteLine();
            }

            for (int j = 0; j < numbers.GetLength(1); j++)
            {
                sum += numbers[rowIndex - 1, j];
            }

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                product *= numbers[i, columnIndex - 1];
            }

            Console.Write($"\nСумма чисел строки под номером {rowIndex} = {sum}\n" +
                          $"Произведение чисел столбца под номером {columnIndex} = {product}\n\n");
        }
    }
}