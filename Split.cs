using System;

/*
 * Дана строка с текстом, используя метод строки String.Split() получить массив слов,
 * которые разделены пробелом в тексте и вывести массив, каждое слово с новой строки.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            string sentence = "На ваше имя пришло письмо. Прочитать его?";

            string[] words;
            char separator = ' ';

            Console.ForegroundColor = ConsoleColor.White;

            words = sentence.Split(separator);

            for (int i = 0; i < words.Length; i++)
                Console.Write($"{words[i]}\n");
        }
    }
}