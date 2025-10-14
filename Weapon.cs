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
        public Player(float health)
        {
            Health = health > 0 ? health : 0;
        }

        public float Health { get; private set; }

        public void TakeDamage(float damage)
        {
            float temporaryHealth;

            if (damage < 0)
                return;

            temporaryHealth = Health - damage;

            Health = temporaryHealth > 0 ? temporaryHealth : 0;
        }
    }

    public class Bot
    {
        private readonly Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            _weapon?.Fire(player);
        }
    }

    public class Weapon
    {
        public Weapon(float damage, float bullets)
        {
            Damage = damage > 0 ? damage : 0;
            Bullets = bullets > 0 ? bullets : 0;
        }

        public float Damage { get; private set; }
        public float Bullets { get; private set; }

        public void Fire(Player player)
        {
            if (Bullets <= 0)
                return;

            player?.TakeDamage(Damage);
            
            Bullets = Bullets > 0 ? --Bullets : 0;
        }
    }
}