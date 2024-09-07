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
            char symbolFilling = '#';
            int maxLengthBar = 10;

            int percentHealthPoint = 67;
            int positionYHealthPoints = 1;

            int percentMagicPoint = 43;
            int positionYMagicPoints = 2;

            DrawBar(maxLengthBar, percentHealthPoint, symbolFilling, ColorRed, positionY: positionYHealthPoints);

            DrawBar(maxLengthBar, percentMagicPoint, symbolFilling, ColorBlue, positionY: positionYMagicPoints);
        }

        static void DrawBar(int maxLengthBar, int persentFilling, char symbolFilling, ConsoleColor colorFilling, int positionX = 0, int positionY = 0)
        {
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