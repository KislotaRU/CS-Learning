namespace CS_JUNIOR
{
    internal class Weapon
    {
        private const int MinCountBullets = 0;

        private int _bullets;

        public bool CanShoot() =>
            _bullets > MinCountBullets;

        public void Shoot() =>
            _bullets--;
    }
}