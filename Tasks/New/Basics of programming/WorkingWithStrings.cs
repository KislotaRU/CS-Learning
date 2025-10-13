using System;

/*
 * Вы задаете вопросы пользователю, по типу "как вас зовут", "какой ваш знак зодиака" и т.д.,
 * и пользователь отвечает на вопросы. После чего, по данным, которые он ввел, формируете небольшой текст о пользователе.
 * Пример текста о пользователе
 * "Вас зовут Алексей, вам 21, вы водолей и работаете на заводе."
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Введите ваше имя: ");
            string userName = Console.ReadLine();

            Console.Write("Ваш возраст: ");
            int userAge = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ваш знак зодиака: ");
            string zodiacSign = Console.ReadLine();

            Console.Write("Где вы работаете: ");
            string placeWork = Console.ReadLine();

            Console.Write("Ваша любимая книга: ");
            string userBook = Console.ReadLine();

            Console.Write("\nИнформация о вас:\n" +
                          $"\tВас зовут {userName}, возраст {userAge}, ваш знак зодиака {zodiacSign}.\n" +
                          $"Работает в {placeWork}. Любимая книга {userBook}.\n");
        }
    }
}
