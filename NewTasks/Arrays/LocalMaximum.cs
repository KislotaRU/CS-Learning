using System;

/*
 * Найдите все локальные максимумы и вывести их. (Элемент является локальным максимумом, если он больше своих соседей)
 * Крайний элемент является локальным максимумом, если он больше своего соседа.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int minNumber = 10;
            int maxNumber = 100;

            int elementsCount = 30;

            int[] numbers = new int[elementsCount];

            int lastIndex = numbers.Length - 1;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Одномерный массив с кол-во элементов {elementsCount}:\n");

            for ( int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minNumber, maxNumber);

                Console.Write($"{numbers[i]} ");
            }

            Console.Write("\n\nЛокальные максимумы одномерного массива:\n\n");

            if (numbers[0] > numbers[1])
                Console.Write($"{numbers[0]} ");

            for (int i = 1; i < numbers.Length - 1; i++)
            {
                if (numbers[i - 1] < numbers[i] && numbers[i] > numbers[i + 1])
                    Console.Write($"{numbers[i]} ");
            }

            if (numbers[lastIndex] > numbers[lastIndex - 1])
                Console.Write($"{numbers[lastIndex]}\n\n");
        }
    }
}