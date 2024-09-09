﻿using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Queue<int> customers = new Queue<int>(new[] { 150, 200, 428, 19, 342, 243 });

            int moneyStore = 0;

            while (customers.Count > 0)
            {
                ShowStore(moneyStore, customers.Count);

                ServeQueue(customers, ref moneyStore);

                Console.ReadKey();
                Console.Clear();
            }

            Console.Write("Все клиенты обслужены.\n");
            Console.ReadKey();
        }

        static void ShowStore(int moneyStore, int customersCount)
        {
            Console.Write("\tМегамаркер\n" +
                         $"Выручка магазина: {moneyStore}$\n" +
                         $"Кол-во клиентов в очереде {customersCount}.\n\n");
        }

        static void ServeQueue(Queue<int> customers, ref int moneyStore)
        {
            int moneyCustomer;

            Console.Write("К вам подошёл клиент.\n" +
                         $"Сумма чека составляет {customers.Peek()}$.\n");

            Console.Write("Нажмите любую кнопку, чтобы обслужить клиента.\n");
            Console.ReadKey();
            Console.Write("Клиент обслужен.\n");

            moneyCustomer = customers.Dequeue();

            AddMoney(ref moneyStore, moneyCustomer);
        }

        static void AddMoney(ref int moneyStore, int moneyToPay)
        {
            if (moneyToPay > 0)
                moneyStore += moneyToPay;
        }
    }
}