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

            if (TryGetValue(weekday, userInput, out int value))
                Console.Write($"День недели {userInput} имеет значение {value}.\n\n");
            else
                Console.Write("Такого дня недели не существует.\n\n");
        }

        static bool TryGetValue(Dictionary<string, int> dictionary, string userInput, out int value)
        {
            value = 0;

            if (dictionary.ContainsKey(userInput))
            {
                value = dictionary[userInput];
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}