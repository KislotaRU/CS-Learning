using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            string name = "Кощей";
            int level = 43;

            Player player = new Player(name, level);

            player.ShowInfo();
        }
    }
}

class Player
{
    private string _name;
    private int _level;

    public Player(string name, int level)
    {
        _name = name;
        _level = level;
    }

    public void ShowInfo() =>
        Console.Write($"Имя: {_name} | Уровень: {_level}\n");
}