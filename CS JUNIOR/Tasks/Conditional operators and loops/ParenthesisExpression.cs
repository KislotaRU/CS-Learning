using System;

namespace CS_JUNIOR
{
    class ParenthesisExpression
    {
        static void Main()
        {
            int countSymbol = 0;
            int maxDepth = 0;
            string text;
            char leftParenthesis = '(';
            char rightParenthesis = ')';

            Console.Write("Введите скобочное выражение: ");
            text = Console.ReadLine();

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == leftParenthesis)
                    countSymbol++;
                else if (text[i] == rightParenthesis)
                    countSymbol--;

                if (countSymbol > maxDepth)
                    maxDepth = countSymbol;

                if (countSymbol < 0)
                    break;
            }

            if (countSymbol == 0)
                Console.WriteLine($"Данное скобочное выражение является корректным. Максимальная глубина вложенности скобок = {maxDepth}.\n");
            else
                Console.WriteLine($"Данное скобочное выражение является НЕкорректным.\n");
        }
    }
}