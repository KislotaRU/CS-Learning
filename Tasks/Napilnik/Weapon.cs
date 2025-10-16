using System;

namespace CS_JUNIOR
{
    public class Player
    {
        public Player(float health)
        {
            Health = health > 0 ? health : throw new ArgumentOutOfRangeException(nameof(health));
        }

        public float Health { get; private set; }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            Health = Math.Max(Health - damage, 0);
        }
    }

    public class Bot
    {
        private readonly Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public void OnSeePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            _weapon.Fire(player);
        }
    }

    public class Weapon
    {
        public Weapon(float damage, float bullets)
        {
            Damage = damage > 0 ? damage : throw new ArgumentOutOfRangeException(nameof(damage));
            Bullets = bullets > 0 ? bullets : throw new ArgumentOutOfRangeException(nameof(bullets));
        }

        public float Damage { get; private set; }
        public float Bullets { get; private set; }

        public void Fire(Player player)
        {
            if (Bullets <= 0)
                throw new ArgumentOutOfRangeException(nameof(Bullets));

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            player.TakeDamage(Damage);

            Bullets--;
        }
    }
}