using System;
using System.IO;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            string[,] map = null;

            string puth = "BraveNewWordMap";

            ReadMap(puth, ref map);
        }

        static void ReadMap(string path, ref string[,] map)
        {
            string[] lines = File.ReadAllLines(path);

            for (int x = 0; x < lines.Length; x++)
                for (int y = 0; y < lines.Length; y++)
                    map[y, y] = lines[y];
        }

        static void DrawMap()
        {

        }
    }
}