using System;

/*
 * При старте программы пользователь вводит начальное количество золота.
 * Потом ему предлагается купить какое-то количество кристаллов по цене N(задать в программе самому).
 * Пользователь вводит число и его золото конвертируется в кристаллы. Остаток золота и кристаллов выводится на экран.
 * Проверять на то, что у игрока достаточно денег не нужно.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            int cristalPrice = 15;
            int coinsCount;
            int cristalsCount;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Введите кол-во золота, которое у вас имеется: ");
            coinsCount = Convert.ToInt16(Console.ReadLine());

            Console.Write("\t---Магический магазин---\n");

            Console.Write("Здесь можно купить кристаллы за золото.\n" +
                          $"Стоимость кристалла составляет: {cristalPrice}\n\n");

            Console.Write("Введите кол-во кристаллов, которое хотите купить: ");
            cristalsCount = Convert.ToInt16(Console.ReadLine());

            coinsCount -= cristalsCount * cristalPrice;

            Console.Write("Ваш балланс.\n" +
                          $"Кол-во золота: {coinsCount}\n" +
                          $"Кол-во кристаллов: {cristalPrice}\n");
        }
    }
}
