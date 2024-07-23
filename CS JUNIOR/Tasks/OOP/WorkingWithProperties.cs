using System;

namespace CS_JUNIOR
{
    class WorkingWithProperties
    {
        static void Main()
        {
            int positionPlayerX = 2;
            int positionPlayerY = 3;
            char symbolPlayer = '%';

            Player player = new Player(positionPlayerX, positionPlayerY, symbolPlayer);
            Renderer renderer = new Renderer();

            renderer.DrawPlayer(player.PositionX, player.PositionY, player.Symbol);
        }
    }

    class Player
    {
        public char Symbol { get; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public Player(int positionX, int positionY, char symbol)
        {
            PositionX = positionX;
            PositionY = positionY;
            Symbol = symbol;
        }
    }

    class Renderer
    {
        public void DrawPlayer(int positionX, int positionY, char symbol)
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(symbol);
        }
    }
}