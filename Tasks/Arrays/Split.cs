using System;

//Дана строка с текстом, используя метод строки String.Split() получить массив слов,
//которые разделены пробелом в тексте и вывести массив, каждое слово с новой строки.

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