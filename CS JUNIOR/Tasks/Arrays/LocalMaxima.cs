using System;

//Дан одномерный массив целых чисел из 30 элементов.
//Найдите все локальные максимумы и вывести их. (Элемент является локальным максимумом, если он больше своих соседей)
//Крайний элемент является локальным максимумом, если он больше своего соседа.
//Программа должна работать с массивом любого размера.
//Массив всех локальных максимумов не нужен.

namespace CS_JUNIOR
{
    class LocalMaxima
    {
        static void Main()
        {
            Random random = new Random();
            int maxRange = 10;
            int minRange = 1;
            int countElements = 30;
            int countLocalMaxElements = 0;
            int[] array = new int[countElements];
            int elementNumberToCheck = 2;

            Console.WriteLine("Исходный массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRange, maxRange);
                Console.Write($"{array[i]} ");
            }

            Console.WriteLine("\n\nЛокальные максимумы: ");

            if (array[0] >= array[1])
            {
                countLocalMaxElements++;
                Console.Write($"{array[0]} ");
            }

            for (int i = 1; i < array.Length - 1; i++)
            {
                if (array[i - 1] <= array[i] && array[i] >= array[i + 1])
                {
                    countLocalMaxElements++;
                    Console.Write($"{array[i]} ");
                }
            }

            if (array[array.Length - 1] >= array[array.Length - elementNumberToCheck])
            {
                countLocalMaxElements++;
                Console.Write($"{array[array.Length - 1]} ");
            }

            Console.WriteLine($"\n\nКоличество локальных максимумов = {countLocalMaxElements}.\n");
        }
    }
}