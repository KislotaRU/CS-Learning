using System;

namespace CS_JUNIOR
{
    class Split
    {
        static void Main()
        {
            string text = "Сортировка пузырьком — один из самых известных алгоритмов сортировки.";

            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine();
        }
    }
}