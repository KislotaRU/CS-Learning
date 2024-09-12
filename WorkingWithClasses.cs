using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            string name = "Кощей";

            Player player = new Player(name);

            player.ShowInfo();
        }
    }
}

class Player
{
    public Player(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public void ShowInfo()
    {
        Console.Write($"Имя: {Name}");
    }
}