using System;

namespace CS_JUNIOR
{
    internal class Program
    {
        public static void Main()
        {
        }

        public static int IndexOf(int[] array, int element)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                    return i;
            }

            return -1;
        }
    }
}
