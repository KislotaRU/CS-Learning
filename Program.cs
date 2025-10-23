using System;

namespace CS_JUNIOR
{
    internal class Program
    {
        public static void Main()
        {
        }

        public static int Clamp(int value, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new InvalidOperationException();

            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }
    }
}
