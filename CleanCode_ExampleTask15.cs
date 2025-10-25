using System.Collections.Generic;

namespace CS_JUNIOR
{
    internal class Player
    {
    }

    internal class Gun
    {
    }

    internal class Follower
    {
    }

    internal class Unit
    {
        public IReadOnlyCollection<Unit> Units { get; private set; }
    }
}