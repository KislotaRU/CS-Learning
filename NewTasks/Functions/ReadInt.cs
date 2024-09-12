using System;

/*
 * Написать функцию, которая запрашивает число у пользователя (с помощью метода Console.ReadLine() )
 * и пытается сконвертировать его в тип int (с помощью int.TryParse())
 * Если конвертация не удалась у пользователя запрашивается число повторно до тех пор,
 * пока не будет введено верно. После ввода, который удалось преобразовать в число, число возвращается. 
*/

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