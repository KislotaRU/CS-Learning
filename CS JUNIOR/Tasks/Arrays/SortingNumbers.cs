using System;

namespace CS_JUNIOR
{
    class SortingNumbers
    {
        static void Main()
        {
            Random random = new Random();
            int minRange = 1;
            int maxRange = 50;
            int countElement = 20;
            int[] array = new int[countElement];

            Console.WriteLine("Исходный массив:\n");

            for (int i = 0; i < countElement; i++)
            {
                array[i] = random.Next(minRange, maxRange);
                Console.Write($"{array[i]} ");
            }

            Console.WriteLine("\n\nМассив после сортировки:\n");

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    int temporaryNumber;

                    if (array[j] > array[j + 1])
                    {
                        temporaryNumber = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temporaryNumber;
                    }
                }
            }

            foreach (int number in array)
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine();

        }
    }
}