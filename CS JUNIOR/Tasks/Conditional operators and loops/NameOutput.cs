using System;

namespace CS_JUNIOR
{
    class NameOutput
    {
        static void Main(string[] args)
        {
            string userName;
            string border = "";
            char borderSymbol;
            int rectangleWidth = 2;
            int fullRectangleWidth;

            Console.Write("Введите имя: ");
            userName = Console.ReadLine();
            Console.Write("Введите символ обводки: ");
            borderSymbol = Convert.ToChar(Console.ReadLine());

            fullRectangleWidth = rectangleWidth + userName.Length;

            for (int i = 0; i < fullRectangleWidth; i++)
            {
                border += borderSymbol;
            }

            Console.WriteLine(border);
            Console.Write(borderSymbol);
            Console.Write(userName);
            Console.WriteLine(borderSymbol);
            Console.WriteLine(border);
        }
    }
}