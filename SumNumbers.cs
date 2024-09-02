using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int maxNumber = 100;
            int number;

            int firstDivider = 3;
            int secondDivider = 5;

            int sumNumbers = 0;

            Console.ForegroundColor = ConsoleColor.White;

            number = random.Next(maxNumber + 1);

            Console.Write($"Рандомное максимальное число: {number}\n");

            for (int i = 0; i <= number; i++)
            {
                if ((i % firstDivider == 0) || (i % secondDivider == 0))
                    sumNumbers += i;
            }

            Console.Write($"Сумма чисел: {sumNumbers}\n");
        }
    }
}