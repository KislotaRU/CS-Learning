using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            string password = "28743W!IUwulW@";
            int attemptsCount = 3;

            string userInput;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Программа хранит секрет. Его можно получить введя верный пароль.\n\n");

            for (int i = attemptsCount; i > 0; i--)
            {
                Console.Write($"Попыток осталось: {i}\n");

                Console.Write("Введите пароль: ");
                userInput = Console.ReadLine();

                if (userInput == password)
                {
                    Console.Write("Пароль верный. Вам доступен секрет.\n\n");
                    break;
                }
                else
                {
                    Console.Write("Пароль неверный.\n\n");
                }
            }
        }
    }
}