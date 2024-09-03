using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            int frameLength = 2;

            string userName;
            char frameSign;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Введите ваше имя: ");
            userName = Console.ReadLine();

            Console.Write("Введите символ обводки вашего имени: ");
            frameSign = Convert.ToChar(Console.ReadLine());

            frameLength += userName.Length;

            for (int i = 0; i < frameLength; i++)
                Console.Write(frameSign);

            Console.Write($"\n{frameSign}{userName}{frameSign}\n");

            for (int i = 0; i < frameLength; i++)
                Console.Write(frameSign);

            Console.WriteLine();
        }
    }
}