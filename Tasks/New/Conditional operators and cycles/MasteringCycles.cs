using System;

/*
 * Напишите простейшую программу, которая выводит указанное(установленное) пользователем сообщение заданное количество раз. 
 * Количество повторов также должен ввести пользователь.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            string userMessage;
            int repetitionsCount;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Программа выводит ваше сообщение указанное вами кол-во раз.\n\n");

            Console.Write("Введите сообщение: ");
            userMessage = Console.ReadLine();

            Console.Write("Введите число повторений: ");
            repetitionsCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < repetitionsCount; i++)
                Console.Write($"{i + 1}. {userMessage}\n");
        }
    }
}