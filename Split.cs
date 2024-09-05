using System;

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