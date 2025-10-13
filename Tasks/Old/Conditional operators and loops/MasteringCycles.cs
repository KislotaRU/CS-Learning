using System;

//При помощи циклов вы можете повторять один и тот же код множество раз.
//Напишите простейшую программу, которая выводит указанное(установленное) 
//пользователем сообщение заданное количество раз. Количество повторов также должен ввести пользователь.

namespace CS_JUNIOR
{
    class MasteringCycles
    {
        static void Main(string[] args)
        {
            string userMessage;
            int сountRepetitionsMessage;

            Console.Write("Введите текст, который хотите повторить: ");
            userMessage = Console.ReadLine();

            Console.Write("Сколько раз повторить текст? Ваше число: ");
            сountRepetitionsMessage = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < сountRepetitionsMessage; i++)
            {
                Console.WriteLine(userMessage);
            }

            Console.WriteLine("\nЦикл закончился.");
        }
    }
}