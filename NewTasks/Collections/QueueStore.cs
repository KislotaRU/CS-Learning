using System;
using System.Collections.Generic;

/*
 * У вас есть множество целых чисел. Каждое целое число - это сумма покупки.
 * Вам нужно обслуживать клиентов до тех пор, пока очередь не станет пуста.
 * После каждого обслуженного клиента деньги нужно добавлять на наш счёт и выводить его в консоль.
 * После обслуживания каждого клиента программа ожидает нажатия любой клавиши, после чего затирает
 * консоль и по новой выводит всю информацию, только уже со следующим клиентом
*/

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

                moneyStore += ServeQueue(customers);

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

        static int ServeQueue(Queue<int> customers)
        {
            int moneyCustomer;

            Console.Write("К вам подошёл клиент.\n" +
                         $"Сумма чека составляет {customers.Peek()}$.\n");

            Console.Write("Нажмите любую кнопку, чтобы обслужить клиента.\n");
            Console.ReadKey();

            moneyCustomer = customers.Dequeue();
            Console.Write("Клиент обслужен.\n");

            return AddMoney(moneyCustomer);
        }

        static int AddMoney(int moneyToPay)
        {
            return moneyToPay > 0 ? moneyToPay : 0;
        }
    }
}