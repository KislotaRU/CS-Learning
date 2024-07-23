using System;

namespace CS_JUNIOR
{
    class WorkingWithClasses
    {
        static void Main()
        {
            string name = "Kislota_RU";
            int health = 100;
            int power = 15;

            Player player = new Player(name, health, power);

            player.ShowStatus();
        }
    }

    class Player
    {
        private string _name;
        private int _health;
        private int _maxHealth;
        private int _power;

        public Player(string name, int health, int power)
        {
            _name = name;
            _health = health;
            _maxHealth = health;
            _power = power;
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Ник - {_name}\nЗдоровье - {_health}/{_maxHealth}\nСила - {_power}");
        }
    }
}