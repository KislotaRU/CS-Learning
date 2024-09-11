using System;
using System.Collections.Generic;

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

            JoinArray(numbers, numbers1);
            JoinArray(numbers, numbers2);

            Console.Write("\nСписок чисел без повторений:\n");
            ShowList(numbers);

            Console.Write("\n\n");
        }

        static void JoinArray(List<int> list, int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                if (list.Contains(array[i]) == false)
                    list.Add(array[i]);
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