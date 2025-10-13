using System;

/*
 * Создать класс игрока, у которого есть данные с его положением в x,y и своим символом.
 * Создать класс отрисовщик, с методом, который получает игрока и отрисовывает его.
 * Используйте автореализуемое свойство.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            char symbol = '@';

            int positionX = 25;
            int positionY = 10;

            Player player = new Player(symbol, positionX, positionY);
            Render render = new Render();

            Console.CursorVisible = false;

            render.DrawPlayer(player);

            Console.ReadKey(true);
        }
    }
}

class Player
{
    public Player(char symbol, int positionX, int positionY)
    {
        Symbol = symbol;
        PositionX = positionX;
        PositionY = positionY;
    }

    public char Symbol { get; private set; }
    public int PositionX { get; private set; }
    public int PositionY { get; private set; }
}

class Render
{
    public void DrawPlayer(Player player)
    {
        Console.SetCursorPosition(player.PositionX, player.PositionY);
        Console.Write(player.Symbol);
    }
}