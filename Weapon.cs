using System;

namespace CS_JUNIOR
{
    public class Program
    {
        static void Main()
        {

        }
    }

    public class Player
    {
        public int Health { get; private set; }

        public Player(int health)
        {
            Health = health;
        }

        public void TakeDamage(int damage)
        {
            int temporaryHealth;

            if (damage < 0)
                return;

            temporaryHealth = Health - damage;

            if (temporaryHealth > 0)
                Health = temporaryHealth;
            else
                Health = 0;
        }
    }

    public class Weapon
    {
        public int Damage { get; private set; }
        public int Bullets { get; private set; }

        public void Fire(Player player)
        {
            player.TakeDamage(Damage);
            Bullets--;
        }
    }

    public class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            _weapon.Fire(player);
        }
    }
}