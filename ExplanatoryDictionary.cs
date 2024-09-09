using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, int> weekday = new Dictionary<string, int>()
            {
                { "Понедельник", 1 },
                { "Вторник", 2 },
                { "Среда", 3 },
                { "Четверг", 4 },
                { "Пятница", 5 },
                { "Суббота", 6 },
                { "Воскресенье", 7 }
            };

            string userInput;

            Console.Write("Введите день недели и получите его значение: ");
            userInput = Console.ReadLine();

            if (weekday.ContainsKey(userInput))
                Console.Write($"День недели {userInput} имеет значение {weekday[userInput]}.\n\n");
            else
                Console.Write("Такого дня недели не существует.\n\n");
        }
    }
}