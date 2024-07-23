using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstName = "Пушкин";
            string lastName = "Александр";

            Console.WriteLine($"---------До перестановки---------\n" +
                              $"Имя - {firstName}.\n" +
                              $"Фамилия - {lastName}.\n");

            string tempName = firstName;
            firstName = lastName;
            lastName = tempName;

            Console.WriteLine($"---------После перестановки---------\n" +
                              $"Имя - {firstName}.\n" +
                              $"Фамилия - {lastName}.\n");
        }
    }
}