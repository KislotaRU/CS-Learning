using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            ConsoleColor colorHealthPoint = ConsoleColor.Red;
            ConsoleColor colorMagicPoint = ConsoleColor.Blue;

            int maxLengthBar = 10;

            int percentagesHealthPoint = 67;
            int positionYHealthPoints = 1;

            int percentagesMagicPoint = 43;
            int positionYMagicPoints = 2;

            DrawBar(maxLengthBar, percentagesHealthPoint, colorHealthPoint, positionY: positionYHealthPoints);

            DrawBar(maxLengthBar, percentagesMagicPoint, colorMagicPoint, positionY: positionYMagicPoints);
        }

        static void DrawBar(int maxLengthBar, int persentFilling, ConsoleColor colorFilling, int positionX = 0, int positionY = 0)
        {
            ConsoleColor colorDefault = Console.ForegroundColor;

            char symbolFilling = '#';
            char symbolEmpty = '_';
            
            int hundredPercent = 100;
            int lengthFilling = persentFilling / (hundredPercent / maxLengthBar);

            Console.SetCursorPosition(positionX, positionY);

            Console.Write($"[");
            Console.ForegroundColor = colorFilling;

            for (int i = 0; i < maxLengthBar; i++)
            {
                if (lengthFilling > i)
                    Console.Write(symbolFilling);
                else
                    Console.Write(symbolEmpty);
            }

            Console.ForegroundColor = colorDefault;
            Console.Write($"]\n");
        }
    }
}