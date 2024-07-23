using System;
using System.IO;

namespace CS_JUNIOR
{
    class BraveNewWorld
    {
        static void Main()
        {
            Console.CursorVisible = false;

            Random randomKey = new Random();
            int minRange = 37;
            int maxRange = 41;

            int playerPositionX = 0;
            int playerPositionY = 0;
            int playerDirectionX = 0;
            int playerDirectionY = 0;
            char symbolPlayer = '@';
            bool isAlive = true;

            int enemyPositionX = 0;
            int enemyPositionY = 0;
            int enemyDirectionX = 0;
            int enemyDirectionY = 0;
            char symbolEnemy = 'E';

            int finishPositionX = 0;
            int finishPositionY = 0;
            char symbolFinish = '%';
            bool isWin = false;

            char symbolWall = '#';
            char symbolEmpty = ' ';

            string mapName = "Map 1";
            char[,] map = ReadMap(mapName, ref playerPositionX, ref playerPositionY, ref enemyPositionX, ref enemyPositionY, ref finishPositionX, ref finishPositionY, symbolPlayer, symbolEnemy, symbolFinish, symbolEmpty);

            bool isPlaying = true;

            DrawMap(map);
            DrawPlayer(symbolPlayer, playerPositionX, playerPositionY);

            while (isPlaying == true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo playerKey = Console.ReadKey(true);
                    ChangeDirection(playerKey, ref playerDirectionX, ref playerDirectionY);

                    if (map[playerPositionX + playerDirectionX, playerPositionY + playerDirectionY] != symbolWall)
                    {
                        Move(symbolPlayer, ref playerPositionX, ref playerPositionY, playerDirectionX, playerDirectionY, map);
                    }
                }

                ChangeDirection(randomKey, ref enemyDirectionX, ref enemyDirectionY, minRange, maxRange);

                if (map[enemyPositionX + enemyDirectionX, enemyPositionY + enemyDirectionY] != symbolWall)
                {
                    Move(symbolEnemy, ref enemyPositionX, ref enemyPositionY, enemyDirectionX, enemyDirectionY, map);
                }
                else
                {
                    ChangeDirection(randomKey, ref enemyDirectionX, ref enemyDirectionY, minRange, maxRange);
                }

                System.Threading.Thread.Sleep(100);

                if (playerPositionX == enemyPositionX && playerPositionY == enemyPositionY)
                {
                    isAlive = false;
                    isPlaying = false;
                }

                if (playerPositionX == finishPositionX && playerPositionY == finishPositionY)
                {
                    isWin = true;
                    isPlaying = false;
                }

            }

            Console.SetCursorPosition(map.GetLength(1), map.GetLength(0));

            if (isAlive == false)
            {
                Console.WriteLine("\nВас съел монстр!");
            }
            else if (isWin == true)
            {
                Console.WriteLine("\nВы нашли выход из лабиринта!");
            }

            Console.WriteLine();
        }

        static void Move(char nextSymbol, ref int positionX, ref int positionY, int directionX, int directionY, char[,] map)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(map[positionX, positionY]);

            positionX += directionX;
            positionY += directionY;

            Console.SetCursorPosition(positionY, positionX);
            Console.Write(nextSymbol);
        }

        static void ChangeDirection(Random randomKey, ref int directionX, ref int directionY, int minRange, int maxRange)
        {
            const ConsoleKey UpArrow = ConsoleKey.UpArrow;
            const ConsoleKey DownArrow = ConsoleKey.DownArrow;
            const ConsoleKey LeftArrow = ConsoleKey.LeftArrow;
            const ConsoleKey RightArrow = ConsoleKey.RightArrow;

            int randomDirection = randomKey.Next(minRange, maxRange);

            switch (randomDirection)
            {
                case (int)UpArrow:
                    directionX = -1;
                    directionY = 0;
                    break;
                case (int)DownArrow:
                    directionX = 1;
                    directionY = 0;
                    break;
                case (int)LeftArrow:
                    directionX = 0;
                    directionY = -1;
                    break;
                case (int)RightArrow:
                    directionX = 0;
                    directionY = 1;
                    break;
            }
        }

        static void ChangeDirection(ConsoleKeyInfo keyInfo, ref int directionX, ref int directionY)
        {
            const ConsoleKey UpArrow = ConsoleKey.UpArrow;
            const ConsoleKey DownArrow = ConsoleKey.DownArrow;
            const ConsoleKey LeftArrow = ConsoleKey.LeftArrow;
            const ConsoleKey RightArrow = ConsoleKey.RightArrow;

            switch (keyInfo.Key)
            {
                case UpArrow:
                    directionX = -1;
                    directionY = 0;
                    break;
                case DownArrow:
                    directionX = 1;
                    directionY = 0;
                    break;
                case LeftArrow:
                    directionX = 0;
                    directionY = -1;
                    break;
                case RightArrow:
                    directionX = 0;
                    directionY = 1;
                    break;
            }
        }

        static void DrawPlayer(char symbolPlayer, int positionX, int positionY)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(symbolPlayer);
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        static char[,] ReadMap(string mapName, ref int playerX, ref int playerY, ref int enemyX, ref int enemyY, ref int finishX, ref int finishY, char symbolPlayer, char symbolEnemy, char symbolFinish, char symbolEmpty)
        {
            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == symbolPlayer)
                    {
                        playerX = i;
                        playerY = j;
                        map[i, j] = symbolEmpty;
                    }
                    else if (map[i, j] == symbolEnemy)
                    {
                        enemyX = i;
                        enemyY = j;
                        map[i, j] = symbolEmpty;
                    }
                    else if (map[i, j] == symbolFinish)
                    {
                        finishX = i;
                        finishY = j;
                    }
                }
            }

            return map;
        }
    }
}