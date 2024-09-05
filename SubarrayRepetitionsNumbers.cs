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

            int repetitionsCount = 0;
            int number = 0;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Массив чисел:\n");

            for (int i = 0; i < arrayNumbers.Length; i++)
            {
                arrayNumbers[i] = random.Next(maxNumber);
                Console.Write($"{arrayNumbers[i]} ");
            }

            for (int i = 0; i < arrayNumbers.Length; i++)
            {
                int temporaryNumber = arrayNumbers[i];
                int temporaryRepetitionsCount = 0;

                for (int j = i; j < arrayNumbers.Length; j++)
                {
                    if (temporaryNumber == arrayNumbers[j])
                        temporaryRepetitionsCount++;
                    else
                        break;
                }

                if (repetitionsCount < temporaryRepetitionsCount)
                {
                    repetitionsCount = temporaryRepetitionsCount;
                    number = temporaryNumber;
                }
            }

            Console.Write($"\n\nБольше всего повтором подряд у числа {number}, повторяется кол-во раз подряд {repetitionsCount}.\n\n");
        }
    }
}