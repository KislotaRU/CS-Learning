using System;

//Написать программу, которая будет выполняться до тех пор, пока не будет введено слово exit.
//Помните, в цикле должно быть условие, которое отвечает за то, когда цикл должен завершиться.
//Это нужно, чтобы любой разработчик взглянув на ваш код, понял четкие границы вашего цикла.

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