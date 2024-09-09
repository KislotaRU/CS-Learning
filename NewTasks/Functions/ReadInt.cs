using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            int number;

            Console.Write("Программа выводит введённое число.\n\n");

            number = ReadInt();

            Console.Write($"Вы ввели число {number}\n");
        }

        static int ReadInt()
        {
            int number;
            string userInput;

            do
            {
                Console.Write("Введите число: ");
                userInput = Console.ReadLine();
            }
            while (int.TryParse(userInput, out number) == false);

            return number;
        }
    }
}