using System;

//Написать функцию, которая запрашивает число у пользователя (с помощью метода Console.ReadLine() ) 
//и пытается сконвертировать его в тип int (с помощью int.TryParse())
//Если конвертация не удалась у пользователя запрашивается число повторно до тех пор, пока не будет введено верно.
//После ввода, который удалось преобразовать в число, число возвращается.
//P.S Задача решается с помощью циклов
//P.S Также в TryParse используется модфикатор параметра out

namespace CS_JUNIOR
{
    class Readint
    {
        static void Main()
        {
            GetInt();
        }

        static int GetInt()
        {
            string text;
            int result = 0;
            bool isResult = false;

            while (isResult == false)
            {
                Console.Write("Введите любое число: ");
                text = Console.ReadLine();

                isResult = int.TryParse(text, out result);

                if (isResult == true)
                {
                    Console.WriteLine($"Число успешно конвертировалось: {result}");
                }
                else
                {
                    Console.WriteLine("Попробуйте ещё раз.");
                }
                
                Console.ReadLine();
                Console.Clear();
            }

            return result;
        }
    }
}