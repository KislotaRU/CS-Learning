using System;

namespace CS_JUNIOR
{
    class DegreeTwo
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minRandomNumber = 1;
            int maxRandomNumber = 1025;
            int number = random.Next(minRandomNumber, maxRandomNumber);
            int degree = 0;
            int numberForDegree = 2;
            int numberInDegree;

            for (numberInDegree = minRandomNumber; numberInDegree < number; numberInDegree *= numberForDegree)
            {
                degree++;
            }

            Console.WriteLine($"Рандомайзер задал число: {number}.\n" +
                              $"Минимальная степень числа {numberForDegree} превосходящее заданное число = {degree}.\n" +
                              $"Число превосходящее заданное число: {numberInDegree}.\n");
        }
    }
}