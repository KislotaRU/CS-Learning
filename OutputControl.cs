using System;

/*
 * Написать программу, которая будет выполняться до тех пор, пока не будет введено слово exit. 
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            string commandExit = "exit";
            
            string userInput = null;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Программа работает до тех пор, пока вы не введёте команду \"exit\".\n\n");

            while (userInput != commandExit)
            {
                Console.Write("Программа работает.\n");

                Console.Write("Ожидается ввод: ");
                userInput = Console.ReadLine();
            }

            Console.Write("\nВы завершили работу программы.\n");
        }
    }
}
