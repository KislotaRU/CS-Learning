using System;

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

            for ( int i = minNumber; i <= maxNumber; i++)
            {
                int temporaryNumber = i;

                while (temporaryNumber > 0)
                    temporaryNumber -= randomNumber;

                if (temporaryNumber == 0)
                    multiplesNumbersCount++;
            }

            Console.Write($"Кол-во чисел кратные {randomNumber} в диапазоне {minNumber}-{maxNumber}(включительно) = {multiplesNumbersCount}.\n");
        }
    }
}