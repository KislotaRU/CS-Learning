using System;
using System.IO;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            ConsoleColor colorDefault = ConsoleColor.White;
            ConsoleColor colorPlayer = ConsoleColor.Yellow;

            char symbolPlayer = '@';
            char symbolItem = 'X';
            char symbolItemEmpty = 'o';
            char symbolWall = '#';
            char symbolEmpty = ' ';

            string path = "BraveNewWordMap";
            char[,] map;

            int score = 0;

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;

            map = ReadMap(path, symbolEmpty, symbolPlayer, symbolItem, out int itemsCount, out int positionPlayerX, out int positionPlayerY);

            while (itemsCount > 0)
            {
                DrawMap(map);
                Console.Write($"Счёт: {score}\n");

                Console.ForegroundColor = colorPlayer;
                DrawPlayer(symbolPlayer, positionPlayerX, positionPlayerY);

                ConsoleKey pressedKey = Console.ReadKey(true).Key;

                SetPositionPlayer(pressedKey, map, symbolWall, ref positionPlayerX, ref positionPlayerY);

                if (map[positionPlayerX, positionPlayerY] == symbolItem)
                {
                    map[positionPlayerX, positionPlayerY] = symbolItemEmpty;
                    itemsCount--;
                    score++;
                }

                Console.ForegroundColor = colorDefault;
                Console.Clear();
            }

            Console.Write("Вы собрали все сокровища!\n");
        }

        static void SetPositionPlayer(ConsoleKey pressedKey, char[,] map, char symbolWall, ref int positionPlayerX, ref int positionPlayerY)
        {
            int temporeryPositionPlayerX = positionPlayerX;
            int temporeryPositionPlayerY = positionPlayerY;

            switch (pressedKey)
            {
                case ConsoleKey.UpArrow:
                    temporeryPositionPlayerX--;
                    break;

                case ConsoleKey.DownArrow:
                    temporeryPositionPlayerX++;
                    break;

                case ConsoleKey.LeftArrow:
                    temporeryPositionPlayerY--;
                    break;

                case ConsoleKey.RightArrow:
                    temporeryPositionPlayerY++;
                    break;
            }

            if (map[temporeryPositionPlayerX, temporeryPositionPlayerY] != symbolWall)
            {
                positionPlayerX = temporeryPositionPlayerX;
                positionPlayerY = temporeryPositionPlayerY;
            }
        }

        static void DrawPlayer(char symbolPlayer, int positionX, int positionY)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(symbolPlayer);
        }

        static char[,] ReadMap(string path, char symbolEmpty, char symbolPlayer, char sybmolItem, out int itemsCount, out int positionPlayerX, out int positionPlayerY)
        {
            itemsCount = 0;
            positionPlayerX = 0;
            positionPlayerY = 0;

            string[] lines = File.ReadAllLines($"{path}.txt");

            char[,] map = new char[lines.Length, lines[0].Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = lines[x][y];

            foreach (char symbol in map)
                if (symbol == sybmolItem)
                    itemsCount++;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == symbolPlayer)
                    {
                        map[x, y] = symbolEmpty;
                        positionPlayerX = x;
                        positionPlayerY = y;
                        break;
                    }
                }

                if (positionPlayerX != 0 && positionPlayerY != 0)
                    break;
            }

            return map;
        }

        static void DrawMap(char[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                    Console.Write(map[x, y]);

                Console.WriteLine();
            }
        }
    }
}