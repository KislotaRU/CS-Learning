﻿using System;
using System.Collections.Generic;

/*
 * Есть два массива строк. Надо их объединить в одну коллекцию, исключив повторения, не используя Linq.
 * Пример: {"1", "2", "1"} + {"3", "2"} => {"1", "2", "3"}
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            int[] numbers1 = new int[]{ 1, 2, 3, 4, 3, 6, 7, 6, 54, 53, 52 };
            int[] numbers2 = new int[]{ 1, 2, 1, 4, 4, 6, 1, 8, 10, 53, 13, 32, 76, 15, 2};

            List<int> numbers = new List<int>();

            Console.Write("Массив первый:\n");
            ShowArray(numbers1);

            Console.Write("\nМассив второй:\n");
            ShowArray(numbers2);

            JoinArray(numbers, numbers1, numbers2);

            Console.Write("\nСписок чисел без повторений:\n");
            ShowList(numbers);

            Console.Write("\n\n");
        }

        static void JoinArray(List<int> list, params int[][] array)
        {
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[i].Length; j++)
                    if (list.Contains(array[i][j]) == false)
                        list.Add(array[i][j]);
        }

        static void ShowArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.Write($"{array[i]} ");
        }

        static void ShowList(List<int> list)
        {
            foreach (int element in list)
                Console.Write($"{element} ");
        }
    }
}