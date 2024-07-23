using System;

namespace CS_JUNIOR
{
    class WorkingWithStrings
    {
        static void Main(string[] args)
        {
            Console.Write("Как вас зовут: ");
            string name = Console.ReadLine();

            Console.Write("Какой ваш любимый фильм: ");
            string titleMovie = Console.ReadLine();

            Console.Write("Где вы работаете: ");
            string workPlace = Console.ReadLine();

            Console.Write("Какой ваш рост в см: ");
            string growth = Console.ReadLine();

            Console.Write("Какой ваш возраст: ");
            string age = Console.ReadLine();

            Console.WriteLine($"\nВас зовут {name} и вам {age}. Также ваш рост состовляет " +
                              $"{growth} см.\nВаш любимый фильм называется \"{titleMovie}\" и вы работаете {workPlace}.\n");
        }
    }
}