using System;

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