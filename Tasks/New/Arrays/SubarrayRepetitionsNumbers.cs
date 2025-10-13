using System;

/*
 * В массиве чисел найдите самый длинный подмассив из одинаковых чисел.
 * Дано 30 чисел. Вывести в консоль сам массив, число, которое само больше раз повторяется подряд и количество повторений.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();
            int maxNumber = 10;

            int numbersCount = 30;
            int[] numbers = new int[numbersCount];

            int minCountRepetitions = 1;
            int maxCountRepetitions = minCountRepetitions;
            int repetitionsCount = minCountRepetitions;
            int number = 0;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Массив чисел:\n");

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(maxNumber);
                Console.Write($"{numbers[i]} ");
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] == numbers[i + 1])
                    repetitionsCount++;
                else
                    repetitionsCount = minCountRepetitions;

                if (maxCountRepetitions < repetitionsCount)
                {
                    maxCountRepetitions = repetitionsCount;
                    number = numbers[i];
                }
            }

            Console.Write($"\n\nБольше всего повтором подряд у числа {number}, повторяется кол-во раз подряд {maxCountRepetitions}.\n\n");
        }
    }
}