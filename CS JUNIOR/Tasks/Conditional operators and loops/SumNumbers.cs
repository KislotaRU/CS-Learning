using System;

//С помощью Random получить число number, которое не больше 100. 
//Найти сумму всех положительных чисел меньше number (включая число), 
//которые кратные 3 или 5. (К примеру, это числа 3, 5, 6, 9, 10, 12, 15 и т.д.)

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