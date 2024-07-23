using System;

namespace CS_JUNIOR
{
    class Pictures
    {
        static void Main(string[] args)
        {
            int picturesRow = 3;
            int allPictures = 52;
            int filledRows = allPictures / picturesRow;
            int restPictures = allPictures % picturesRow;
            Console.WriteLine($"Полностью заполненных строк: {filledRows}." +
                              $"\nКоличество невошедших картинок: {restPictures}.");
        }
    }
}