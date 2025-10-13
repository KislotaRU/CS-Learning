using System;

/*
 * Вывести имя в прямоугольник из символа, который введет сам пользователь.
 * От пользователя получаете символ и имя и по этим данным выводите имя в прямоугольнике.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            string border = "";
            string middleLine = "";

            string userName;
            char frameSign;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Введите ваше имя: ");
            userName = Console.ReadLine();

            Console.Write("Введите символ обводки вашего имени: ");
            frameSign = Console.ReadKey(true).KeyChar;

            middleLine = frameSign + userName + frameSign;

            for (int i = 0; i < middleLine.Length; i++)
                border += frameSign;

            Console.Write($"\n{border}");
            Console.Write($"\n{middleLine}");
            Console.Write($"\n{border}\n");
        }
    }
}
