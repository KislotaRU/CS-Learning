using System;

namespace CS_JUNIOR
{
    class Program
    {
        const ConsoleColor ColorDefault = ConsoleColor.Gray;
        const ConsoleColor ColorRed = ConsoleColor.Red;
        const ConsoleColor ColorBlue = ConsoleColor.Blue;

        const int HundredPercent = 100;

        static void Main()
        {
            int maxLengthBar = 10;

            int percentagesHealthPoint = 67;
            int positionYHealthPoints = 1;

            int percentagesMagicPoint = 43;
            int positionYMagicPoints = 2;

            DrawBar(maxLengthBar, percentagesHealthPoint, ColorRed, positionY: positionYHealthPoints);

            DrawBar(maxLengthBar, percentagesMagicPoint, ColorBlue, positionY: positionYMagicPoints);
        }

        static void DrawBar(int maxLengthBar, int persentFilling, ConsoleColor colorFilling, int positionX = 0, int positionY = 0)
        {
            char symbolFilling = '#';
            char symbolEmpty = '_';

            int lengthFilling = persentFilling / (HundredPercent / maxLengthBar);

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

            Console.ForegroundColor = ColorDefault;
            Console.Write($"]\n");
        }
    }
}