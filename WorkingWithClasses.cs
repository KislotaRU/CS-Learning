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
    public Player(string name, int level)
    {
        Name = name;
        Level = level;
    }
        
    public string Name { get; private set; }
    public int Level { get; private set; }

    public void ShowInfo() =>
        Console.Write($"Имя: {Name} | Уровень: {Level}\n");
}