using System;

namespace CS_JUNIOR
{
    internal class Weapon
    {
        private const int NumberBulletsPerShoot = 1;

        private int _bullets;

        public Weapon(int bullets)
        {
            _bullets = bullets >= 0 ? bullets : throw new ArgumentOutOfRangeException(nameof(bullets));
        }

        public bool CanShoot() =>
            _bullets >= NumberBulletsPerShoot;

        public void Shoot()
        {
            if (CanShoot())
                _bullets -= NumberBulletsPerShoot;
        }
    }
}