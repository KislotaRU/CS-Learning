using System;

namespace CS_JUNIOR
{
    class ShiftingArrayValues
    {
        static void Main()
        {
            Random random = new Random();
            int minRange = 1;
            int maxRange = 5;
            int countElements = 10;
            int[] array = new int[countElements];
            int countShifts;

            Console.WriteLine("Исходный массив:\n");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRange, maxRange);
                Console.Write($"{array[i]} ");
            }

            Console.Write("\n\nВведите значение, на которое нужно сдвинуть массив влево: ");
            countShifts = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < countShifts; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    int temporaryNumber;

                    temporaryNumber = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temporaryNumber;
                }
            }

            Console.WriteLine();

            foreach (int number in array)
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine("\n");
        }
    }
}