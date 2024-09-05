using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int shiftLength;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Программа сдвигает числа в массиве влево на указанное число позиций.\n");

            Console.Write("Массив:\n\n");

            foreach (int number in numbers)
                Console.Write($"{number} ");

            Console.Write("\n\nВведите число: ");

            shiftLength = Convert.ToInt32(Console.ReadLine());

            shiftLength = shiftLength % numbers.Length;

            for (int i = 0; i < shiftLength; i++)
            {
                int temporaryNumber;

                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    temporaryNumber = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temporaryNumber;
                }
            }
            
            Console.Write("\n\n");

            foreach (int number in numbers)
                Console.Write($"{number} ");

            Console.Write("\n\n");
        }
    }
}