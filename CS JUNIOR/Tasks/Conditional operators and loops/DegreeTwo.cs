using System;

//Найдите минимальную степень двойки, превосходящую заданное число.
//К примеру, для числа 4 будет 2 в степени 3, то есть 8. 4<8.
//Для числа 29 будет 2 в степени 5, то есть 32. 29<32.
//В консоль вывести число (лучше получить от Random), степень и само число 2 в найденной степени.

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