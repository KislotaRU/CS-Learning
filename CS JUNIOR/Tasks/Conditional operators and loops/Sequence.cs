using System;

//Нужно написать программу (используя циклы, обязательно пояснить выбор вашего цикла), 
//чтобы она выводила следующую последовательность 5 12 19 26 33 40 47 54 61 68 75 82 89 96
//Нужны переменные для обозначения чисел в условии цикла.

namespace CS_JUNIOR
{
    class Sequence
    {
        static void Main(string[] args)
        {
            int startOfSequence = 5;
            int endOfSequence = 96;
            int stepSequence = 7;

            for (int i = startOfSequence; i <= endOfSequence; i += stepSequence)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }
    }
}