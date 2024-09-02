using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int maxNumber = 100;
            int randomNumber;

            int firstDivider = 3;
            int secondDivider = 5;

            int sumNumbers = 0;

            Console.ForegroundColor = ConsoleColor.White;

            randomNumber = random.Next(maxNumber + 1);

            Console.Write($"Рандомное максимальное число: {randomNumber}\n");

            for (int i = 0; i <= randomNumber; i++)
            {
                if ((i % firstDivider == 0) && (i % secondDivider == 0))
                    sumNumbers += i;
            }

            Console.Write($"Сумма чисел: {sumNumbers}\n");
        }
    }
}