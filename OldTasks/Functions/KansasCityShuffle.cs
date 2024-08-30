using System;

//Реализуйте функцию Shuffle, которая перемешивает элементы массива в случайном порядке.

namespace CS_JUNIOR
{
    class KansasCityShuffle
    {
        static void Main()
        {
            int[] numbers = new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            
            PrintArray(numbers);

            ShuffleArray(numbers);

            PrintArray(numbers);
        }

        static void ShuffleArray(int[] array)
        {
            Random random = new Random();
            int firstNumber;
            int secondNumber;
            int temporaryNumber;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    firstNumber = random.Next(0, array.Length);
                    secondNumber = random.Next(0, array.Length);

                    temporaryNumber = array[firstNumber];
                    array[firstNumber] = array[secondNumber];
                    array[secondNumber] = temporaryNumber;
                }
            }
        }

        static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }

            Console.WriteLine();
        }
    }
}