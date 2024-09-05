using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            char parenthesisLeft = '(';
            char parenthesisRight = ')';

            string line;

            int maxDepthLine = 0;
            int parenthesisCount = 0;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Введите скобочное выражение: ");
            line = Console.ReadLine();

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == parenthesisLeft)
                    parenthesisCount++;
                else if (line[i] == parenthesisRight)
                    parenthesisCount--;

                if (maxDepthLine < parenthesisCount)
                    maxDepthLine = parenthesisCount;

                if (parenthesisCount < 0)
                    break;
            }

            if (parenthesisCount == 0)
                Console.Write($"\nСкобочное выражение корректное, максимальная глубина составляет {maxDepthLine}.\n\n");
            else
                Console.Write($"\nСкобочное выражение некорректное.\n\n");
        }
    }
}