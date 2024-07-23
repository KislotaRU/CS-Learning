using System;

namespace CS_JUNIOR
{
    class SumNumbers
    {
        static void Main(string[] args)
        {
            int sumOfNumbers = 0;
            int maximumRandomNumber = 101;
            int divisor1 = 3;
            int divisor2 = 5;
            Random random = new Random();
            int number = random.Next(maximumRandomNumber);

            for (int i = 0; i <= number; i++)
            {
                if ((i % divisor1 == 0) || (i % divisor2 == 0))
                {
                    sumOfNumbers += i;
                }
            }

            Console.WriteLine(sumOfNumbers);
        }
    }
}