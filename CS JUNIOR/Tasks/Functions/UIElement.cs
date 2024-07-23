using System;

namespace CS_JUNIOR
{
    class UIElement
    {
        static void Main()
        {
            int positionX = 1;
            int positionY = 0;
            float percentagesHealth;
            float percentagesMana;
            ConsoleColor colorHealth = ConsoleColor.DarkRed;
            ConsoleColor colorMana = ConsoleColor.DarkBlue;

            Console.WriteLine("Введите кол-во процентов здоровья: ");
            percentagesHealth = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите кол-во процентов маны: ");
            percentagesMana = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            DrawBar(percentagesHealth, colorHealth);
            DrawBar(percentagesMana, colorMana, positionX: positionX);

            Console.ReadLine();
        }

        static void DrawBar(float percentagesFilled, ConsoleColor colorFill, int maxCountDivisions = 10, int positionX = 0, int positionY = 0)
        {
            ConsoleColor dafault = Console.ForegroundColor;
            char symbolFill = '#';
            char symbolEmptiness = '_';
            int countFilledDivisions;
            int maxPercent = 100;

            countFilledDivisions = (int)(percentagesFilled / maxPercent * maxCountDivisions);

            Console.SetCursorPosition(positionY, positionX);
            Console.Write("[");
            Console.ForegroundColor = colorFill;

            for (int i = 0; i < maxCountDivisions; i++)
            {
                if (countFilledDivisions > i)
                    Console.Write(symbolFill);
                else
                    Console.Write(symbolEmptiness);
            }

            Console.ForegroundColor = dafault;
            Console.Write("]\n");
        }
    }
}