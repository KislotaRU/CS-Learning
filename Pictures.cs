using System;

/*
 * На экране, в специальной зоне, выводятся картинки, по 3 в ряд (условно, ничего рисовать не надо).
 * Всего у пользователя в альбоме 52 картинки. Код должен вывести, сколько полностью заполненных
 * рядов можно будет вывести, и сколько картинок будет сверх меры.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            int picturesCount = 52;
            int maxCountPictures = 3;

            int rowsFilledCount = picturesCount / maxCountPictures;
            int remainderPictures = picturesCount % maxCountPictures;

            Console.Write($"Кол-во заполненных рядов: {rowsFilledCount}\n" +
                          $"Кол-во оставшихся картинок: {remainderPictures}\n");
        }
    }
}
