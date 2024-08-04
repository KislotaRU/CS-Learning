using System;

//Даны две переменные. Поменять местами значения двух переменных. Вывести на экран значения переменных до перестановки и после.
//К примеру, есть две переменные имя и фамилия, они сразу инициализированные, 
//но данные не верные, перепутанные. Вот эти данные и надо поменять местами через код.

namespace CS_JUNIOR
{
    class SwappingValues
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