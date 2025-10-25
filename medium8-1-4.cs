using System;

namespace CS_JUNIOR
{
    internal interface IWeapon
    {
        void Attack();
    }

    internal interface IMovement
    {
        void Move();
    }

    internal class Player
    {
        private readonly IWeapon _weapon;
        private readonly IMovement _movement;

        public Player(IWeapon weapon, IMovement movement, int age, string name)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            Age = age >= 0 ? age : throw new ArgumentOutOfRangeException(nameof(age));
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException(nameof(name)) : name;
        }

        public int Age { get; private set; }

        public string Name { get; private set; }

        public void Move()
        {
            _movement.Move();
        }

        public void Attack()
        {
            _weapon.Attack();
        }
    }

    internal class Weapon : IWeapon
    {
        public Weapon(int damage, float cooldown)
        {
            Damage = damage >= 0 ? damage : throw new ArgumentOutOfRangeException(nameof(damage));
            Cooldown = cooldown >= 0 ? cooldown : throw new ArgumentOutOfRangeException(nameof(cooldown));
        }

        public int Damage { get; private set; }

        public float Cooldown { get; private set; }

        public void Attack()
        {
            if (IsReloading())
            {
                throw new NotImplementedException();
            }
        }

        private bool IsReloading()
        {
            throw new NotImplementedException();
        }
    }

    internal class Movement : IMovement
    {
        public Movement(float speed)
        {
            Speed = speed >= 0 ? speed : throw new ArgumentOutOfRangeException(nameof(speed));
        }

        public float Speed { get; private set; }

        public float DirectionX { get; private set; }

        public float DirectionY { get; private set; }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}