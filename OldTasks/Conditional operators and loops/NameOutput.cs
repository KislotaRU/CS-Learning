using System;

//Вывести имя в прямоугольник из символа, который введет сам пользователь.
//Вы запрашиваете имя, после запрашиваете символ, а после отрисовываете в консоль его имя в прямоугольнике из его символов.
//Пример:
//Alexey
//%%%%%%%%
//%Alexey%
//%%%%%%%%
//Примечание:
//Длину строки можно всегда узнать через свойство Length
//string someString = “Hello”;
//Console.WriteLine(someString.Length); //5

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