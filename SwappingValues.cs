using System;

/*
 * Даны две переменные. Поменять местами значения двух переменных. 
 * Вывести на экран значения переменных до перестановки и после.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            string firstName = "Гоголь";
            string lastName = "Николай";

            string temporaryName; 

            Console.Write("Данные до.\n" +
                          $"Имя: {firstName}\n" +
                          $"Фамилия: {lastName}\n");

            temporaryName = firstName;
            firstName = lastName;
            lastName = temporaryName;

            Console.Write("\nДанные после.\n" +
                          $"Имя: {firstName}\n" +
                          $"Фамилия: {lastName}\n");
        }
    }
}
