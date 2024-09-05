using System;

/*
 * Дано N (10 ≤ N ≤ 25).
 * Найти количество чисел от 50 до 150 (включая эти числа), которые кратны N.
 * Операции деления (/, %) не использовать. А умножение не требуется.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int minRandomNumber = 10;
            int maxRandomNumber = 25;
            int randomNumber;

            int minNumber = 50;
            int maxNumber = 150;

            int multiplesNumbersCount = 0;

            randomNumber = random.Next(minRandomNumber, maxRandomNumber + 1);

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Число N: {randomNumber}\n");

            for ( int i = 0; i <= maxNumber; i += randomNumber)
            {
                if (i >= minNumber)
                    multiplesNumbersCount++;
            }

            Console.Write($"Кол-во чисел кратные {randomNumber} в диапазоне {minNumber}-{maxNumber}(включительно) = {multiplesNumbersCount}.\n");
        }
    }
}