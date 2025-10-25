using System;

namespace CS_JUNIOR
{
    internal class CleanCode_ExampleTask34
    {
        public void Shoot(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            throw new NotImplementedException();
        }

        public string FindBy(int index)
        {
            if (index >= 0)
                throw new ArgumentNullException(nameof(index));

            throw new NotImplementedException();
        }
    }
}