using System;

namespace CS_JUNIOR
{
    class OutputControl
    {
        static void Main(string[] args)
        {
            string inputUser = "";
            string commandExit = "exit";

            while (inputUser != commandExit)
            {
                Console.WriteLine("Начало цикла");

                Console.WriteLine($"Вы находитесь в цикле while. Для выхода введите слово \"{commandExit}\".");
                inputUser = Console.ReadLine();

                Console.WriteLine("Конец цикла");
            }

            Console.WriteLine("Вы вышли из цикла.");
        }
    }
}