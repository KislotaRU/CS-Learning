using System;

//Дан двумерный массив.
//Вычислить сумму второй строки и произведение первого столбца.
//Вывести исходную матрицу и результаты вычислений.

namespace CS_JUNIOR
{
    class WorkingWithSpecificRowsAndColumns
    {
        static void Main()
        {
            Random random = new Random();
            int maxRange = 10;
            int minRange = 1;
            int countRows = 5;
            int countColomns = 5;
            int[,] array = new int[countRows, countColomns];
            int sumOfNumbers = 0;
            int productOfNumber = 1;
            int rowForCounting = 1;
            int colomnForCounting = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(minRange, maxRange);
                    Console.Write($"{array[i, j]} ");
                }

                Console.WriteLine();
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                productOfNumber *= array[i, colomnForCounting]; 
            }

            for (int i = 0; i < array.GetLength(1); i++)
            {
                sumOfNumbers += array[rowForCounting, i];
            }

            Console.WriteLine($"\nСумма второй строки = {sumOfNumbers}.\n" +
                              $"Произведение первого столбца = {productOfNumber}.\n");
        }
    }
}