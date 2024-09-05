using System;

/*
 * Создайте переменную типа string, в которой хранится пароль для доступа к тайному сообщению.
 * Пользователь вводит пароль, далее происходит проверка пароля на правильность, и если пароль
 * неверный, то попросите его ввести пароль ещё раз. Если пароль подошёл, выведите секретное сообщение.
 * Если пользователь неверно ввел пароль 3 раза, программа завершается.
*/

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