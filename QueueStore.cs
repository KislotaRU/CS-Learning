using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Queue<int> customers = new Queue<int>(new[] { 150, 200, 428, 19, 342, 243 });

            int moneyToPay;

            int moneyStore = 0;

            while (customers.Count > 0)
            {
                Console.Write("\tМегамаркер\n");

                Console.Write($"Выручка магазина: {moneyStore}\n");
                Console.Write($"Кол-во клиентов в очереде {customers.Count}.\n\n");

                moneyToPay = customers.Dequeue();
                Console.Write("К вам подошёл клиент.\n" +
                              $"Сумма чека составляет {moneyToPay}.\n");

                Console.Write("Нажмите любую кнопку, чтобы обслужить клиента.\n");
                Console.ReadKey();

                Console.Write("Клиент обслужен.\n");

                if (moneyToPay > 0)
                    moneyStore += moneyToPay;

                Console.Write("В кассу магазина поступили деньги.\n");

                Console.ReadKey();
                Console.Clear();
            }

            Console.Write("Все клиента обслужены.\n");
            Console.ReadKey();
        }
    }
}