using System;

/*
 * Найдите минимальную степень двойки, превосходящую заданное число.
 * К примеру, для числа 4 будет 2 в степени 3, то есть 8. 4<8.
 * Для числа 29 будет 2 в степени 5, то есть 32. 29<32.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int maxNumber = 10000;
            int randomNumber;

            int degreeNumber = 0;
            int number = 2;
            int result = 1;

            randomNumber = random.Next(maxNumber);

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Заданно число: {randomNumber}\n");

            while (randomNumber >= result)
            {
                degreeNumber++;
                result *= number;
            }

            Console.Write($"{number} в степени {degreeNumber}, что явялеется числом {result} превосходит заданное число {randomNumber}.\n");
        }
    }
}