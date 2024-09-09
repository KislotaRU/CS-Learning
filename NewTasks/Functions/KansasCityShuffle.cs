using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Console.Write("Исхдный массив:\n");
            PrintArray(numbers);

            Shuffle(numbers);

            Console.Write("Перемешанный массив:\n");
            PrintArray(numbers);
        }

        static void Shuffle(int[] numbers)
        {
            Random random = new Random();

            int temporaryNumber;
            int randomIndex;

            for (int i = 0; i < numbers.Length; i++)
            {
                randomIndex = random.Next(numbers.Length);

                temporaryNumber = numbers[i];
                numbers[i] = numbers[randomIndex];
                numbers[randomIndex] = temporaryNumber;
            }
        }

        static void PrintArray(int[] numbers)
        {
            foreach (int number in numbers)
                Console.Write($"{number} ");

            Console.Write("\n\n");
        }
    }
}