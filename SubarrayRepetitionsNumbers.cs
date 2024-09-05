using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();
            int maxNumber = 10;

            int numbersCount = 30;
            int[] arrayNumbers = new int[numbersCount];

            int minCountRepetitions = 1;
            int maxCountRepetitions = minCountRepetitions;
            int repetitionsCount = minCountRepetitions;
            int number = 0;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Массив чисел:\n");

            for (int i = 0; i < arrayNumbers.Length; i++)
            {
                arrayNumbers[i] = random.Next(maxNumber);
                Console.Write($"{arrayNumbers[i]} ");
            }

            for (int i = 0; i < arrayNumbers.Length - 1; i++)
            {
                if (arrayNumbers[i] == arrayNumbers[i + 1])
                    repetitionsCount++;
                else
                    repetitionsCount = minCountRepetitions;

                if (maxCountRepetitions < repetitionsCount)
                {
                    maxCountRepetitions = repetitionsCount;
                    number = arrayNumbers[i];
                }
            }

            Console.Write($"\n\nБольше всего повтором подряд у числа {number}, повторяется кол-во раз подряд {maxCountRepetitions}.\n\n");
        }
    }
}