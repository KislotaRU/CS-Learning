using System;

//В массиве чисел найдите самый длинный подмассив из одинаковых чисел.
//Дано 30 чисел. Вывести в консоль сам массив, число, которое само больше раз повторяется подряд и количество повторений.
//Дополнительный массив не надо создавать.
//Пример: { 5, 5, 9, 9, 9, 5, 5}
//-число 9 повторяется большее число раз подряд.

namespace CS_JUNIOR
{
    class SubarrayRepetitionsNumbers
    {
        static void Main()
        {
            Random random = new Random();
            int minRange = 9;
            int maxRange = 11;
            int countElement = 30;
            int[] array = new int[countElement];
            int repeatingNumber = 0;
            int maxCountRepetitions = 0;
            int countNumbers = 0;
            int reset = 1;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRange, maxRange);
                Console.Write($"{array[i]} ");
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == array[i + 1])
                    countNumbers++;
                else
                    countNumbers = reset;

                if (countNumbers > maxCountRepetitions)
                {
                    repeatingNumber = array[i];
                    maxCountRepetitions = countNumbers;
                }
            }

            Console.WriteLine($"\n\nЧисло {repeatingNumber} повторяется большее число раз подряд." +
                              $"\nКол-во повторений = {maxCountRepetitions}.");
        }
    }
}