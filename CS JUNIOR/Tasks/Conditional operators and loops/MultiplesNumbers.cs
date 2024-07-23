using System;

namespace CS_JUNIOR
{
    class MultiplesNumbers
    {
        static void Main()
        {
            Random random = new Random();
            int numberMin = 1;
            int numberMax = 28;
            int number = random.Next(numberMin, numberMax);
            int multiplesCount = 0;
            int minRange = 100;
            int maxRange = 999;

            Console.WriteLine($"Число N = {number}.");

            for (int i = 0; i <= maxRange; i += number)
            {
                if (i >= minRange)
                    multiplesCount++;
            }

            Console.WriteLine($"{multiplesCount} - количество трёхзначных чисел кратные числу {number}.\n");
        }
    }
}