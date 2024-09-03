using System;

/*
 * Надо написать программу (используя циклы, обязательно пояснить выбор вашего цикла),
 * чтобы она выводила следующую последовательность 5 12 19 26 33 40 47 54 61 68 75 82 89 96 103
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            int stepSize = 7;
            int minNumber = 5;

            int maxNumber;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Программа выводит последовательность чисел до указанного максимума.\n\n");

            Console.Write("Введите максимальное число: ");
            maxNumber = Convert.ToInt32(Console.ReadLine());

            for (int i = minNumber; i <= maxNumber; i += stepSize)
                Console.Write($"{i}\n");
        }
    }
}