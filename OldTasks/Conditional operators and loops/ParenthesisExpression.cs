using System;

//Дана строка из символов '(' и ')'. Определить, является ли она корректным скобочным выражением. Определить максимальную глубину вложенности скобок.
//Пример “(()(()))” - строка корректная и максимум глубины равняется 3.
//Пример не верных строк: "(()", "())", ")(", "(()))(()"
//Для перебора строки по символам можно использовать цикл foreach, к примеру будет так foreach (var symbol in text)
//Или цикл for(int i = 0; i < text.Length; i++) и дальше обращаться к каждому символу внутри цикла как text[i]
//Цикл нужен для перебора всех символов в строке.

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