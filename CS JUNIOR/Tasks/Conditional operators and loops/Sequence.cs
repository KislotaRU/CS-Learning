using System;

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