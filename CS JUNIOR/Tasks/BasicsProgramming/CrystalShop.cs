using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main(string[] args)
        {
            int goldMoneyCount;
            int cristalsCount;
            int cristalPrice = 13;

            Console.Write("Введите количество золота у Вас в кошельке: ");
            goldMoneyCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Сколько кристалов Вы хотите купить?");
            cristalsCount = Convert.ToInt32(Console.ReadLine());

            goldMoneyCount -= cristalsCount * cristalPrice;

            Console.WriteLine($"Ваш остаток золота: {goldMoneyCount}.\n" +
                              $"Ваши кристаллы: {cristalsCount}.");
        }
    }
}